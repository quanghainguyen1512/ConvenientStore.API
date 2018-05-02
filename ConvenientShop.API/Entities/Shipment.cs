namespace ConvenientShop.API.Entities
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public Delivery DeliveryDate { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}