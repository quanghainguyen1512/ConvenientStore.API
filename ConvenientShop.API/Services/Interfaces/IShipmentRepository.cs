using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IShipmentRepository
    {
        IEnumerable<Shipment> GetAllShipments();
        IEnumerable<Shipment> GetShipmentsForDelivery(int deliId);
        Shipment GetShipment(int shipmentId, bool includeDetail);
        bool AddShipment(Shipment shipment);
        bool DeleteShipment(Shipment shipment);
    }
}