using System.Collections.Generic;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersForStaff(int staffId);
        IEnumerable<OrderDetail> GetOrderDetailsForOrder(int orderId);
        Order GetOrder(int orderId, bool includeDetails);
        bool AddOrder(Order orderToAdd);
        bool OrderDetailExists(int id, out int quantityinrepo);
    }
}