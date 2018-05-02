using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IShipmentRepository
    {
        IEnumerable<Shipment> GetAllShipments();
        Shipment GetShipment(int shipmentId, bool includeDetail);
        bool AddShipment(Shipment shipment);
        bool DeleteShipment(Shipment shipment);
        bool AddProductDetailToShipment(int shipmentId, ProductDetail detail);
        bool AddProductDetailsToShipment(int shipmentId, IEnumerable<ProductDetail> detail);
    }
}