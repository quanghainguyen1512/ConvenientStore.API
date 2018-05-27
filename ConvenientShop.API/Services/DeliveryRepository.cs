using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using Microsoft.Extensions.Options;
using Z.Dapper.Plus;

namespace ConvenientShop.API.Services
{
    public class DeliveryRepository : ConvenientStoreRepository, Interfaces.IDeliveryRepository
    {
        public DeliveryRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AddDelivery(Delivery delivery)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {

                    DapperPlusManager.Entity<Delivery>()
                        .Table("delivery")
                        .Identity(d => d.DeliveryId)
                        .Ignore(d => d.Supplier)
                        .Ignore(d => d.Shipments);
                    DapperPlusManager.Entity<Shipment>()
                        .Table("shipment")
                        .Identity(s => s.ShipmentId)
                        .Ignore(s => s.ProductDetail)
                        .Ignore(s => s.Delivery)
                        .Ignore(s => s.OrderDetail)
                        .Map(s => new
                        {
                            s.ShipmentId,
                                DeliveryId = s.Delivery.DeliveryId,
                                OrderDetailId = s.OrderDetail.OrderDetailId,
                                BarCode = s.ProductDetail.BarCode
                        });
                    DapperPlusManager.Entity<ProductDetail>()
                        .Table("product_detail")
                        .Key(pd => pd.BarCode)
                        .Ignore(pd => pd.Product);
                    DapperPlusManager.Entity<OrderDetail>()
                        .Table("order_detail")
                        .Key(od => od.OrderDetailId)
                        .Map(od => new
                        {
                            od.OrderDetailId,
                                IsReceived = true
                        });

                    try
                    {
                        tran.BulkInsert(delivery)
                            .ThenForEach(d => d.Shipments.ForEach(s =>
                            {
                                s.Delivery = d;
                                s.ProductDetail.QuantityInRepository = s.OrderDetail.ProductQuantity;
                            }))
                            .Include(x => x.ThenBulkInsert(d => d.Shipments.Select(s => s.ProductDetail)))
                            .Include(x => x.ThenBulkUpdate(d => d.Shipments.Select(s => s.OrderDetail)))
                            .ThenBulkInsert(d => d.Shipments);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                    return true;
                }
            }
        }

        public bool DeleteDelivery(Delivery delivery)
        {
            using(var tran = new TransactionScope())
            {
                using(var conn = Connection)
                {
                    conn.Open();
                }
                tran.Complete();
                return false;
            }
        }

        public IEnumerable<Delivery> GetAllDelivery()
        {
            using(var conn = Connection)
            {
                conn.Open();
                // var sql = "SELECT d.DeliveryId, d.DeliveryDate, d.Cost, s.SupplierId, s.SupplierName " +
                //     "FROM dbo.delivery AS d " +
                //     "INNER JOIN dbo.supplier AS s ON d.SupplierId = s.SupplierId";
                // return conn.Query<Delivery, Supplier>(sql, splitOn: "SupplierId");
                return conn.GetAll<Delivery>();
            }
        }

        // Haven't Done
        public Delivery GetDelivery(int deliveryId, bool includeShipment)
        {
            using(var conn = Connection)
            {
                conn.Open();
                if (!includeShipment)
                    return conn.Get<Delivery>(deliveryId);

                var sql = "SELECT d.DeliveryId, d.DeliveryDate, d.Cost, " +
                    " " +
                    "FROM dbo.delivery AS d " +
                    "INNER JOIN dbo.supplier AS su ON su.SupplierId = d.Supplier " +
                    "INNER JOIN dbo.shipment AS sh ON d.DeliveryId = s.DeliveryId " +
                    "WHERE d.DeliveryId = @deliveryId";
                return conn.Query<Delivery, Shipment>(
                    sql,
                    splitOn: "",
                    param : new { deliveryId }
                ).FirstOrDefault();
            }
        }
    }
}