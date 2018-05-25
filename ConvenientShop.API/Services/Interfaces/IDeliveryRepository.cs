using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IDeliveryRepository
    {
        IEnumerable<Delivery> GetAllDelivery();
        Delivery GetDelivery(int deliveryId, bool includeShipment);
        bool AddDelivery(Delivery delivery);
        bool DeleteDelivery(Delivery delivery);
    }
}