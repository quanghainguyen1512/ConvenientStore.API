using System.Collections.Generic;
using System.Linq;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class ShipmentRepository : ConvenientStoreRepository, Interfaces.IShipmentRepository
    {
        public ShipmentRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AddShipment(Shipment shipment)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    var result = conn.Insert(shipment) != 0 && conn.Insert(shipment.ProductDetail) != 0;
                    tran.Commit();
                    return result;
                }
            }
        }

        public bool DeleteShipment(Shipment shipment)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    var result = conn.Delete(shipment) && conn.Delete(shipment.ProductDetail);
                    tran.Commit();
                    return result;
                }
            }
        }

        public IEnumerable<Shipment> GetAllShipments()
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.GetAll<Shipment>();
            }
        }

        public Shipment GetShipment(int shipmentId, bool includeDetail)
        {
            using(var conn = Connection)
            {
                conn.Open();
                if (!includeDetail)
                    return conn.Get<Shipment>(shipmentId);
                var sql = "SELECT s.ShipmentId, od.OrderDetailId, od.ProductQuantity, p.Name " +
                    "FROM shipment AS s " +
                    "INNER JOIN order_detail AS od ON s.OrderDetailId = od.OrderDetailId " +
                    "INNER JOIN product AS p ON od.ProductId = p.ProductId " +
                    "WHERE s.ShipmentId = @shipmentId";
                return conn.Query<Shipment, OrderDetail, Product>(
                    sql,
                    splitOn: "",
                    param : new { shipmentId }
                ).FirstOrDefault();
            }
        }

        public IEnumerable<Shipment> GetShipmentsForDelivery(int deliId)
        {
            using(var conn = Connection)
            {
                conn.Open();

                return null;
            }
        }
    }
}