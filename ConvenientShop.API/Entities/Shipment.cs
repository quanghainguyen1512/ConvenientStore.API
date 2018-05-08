using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("shipment")]
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }
        public int DeliveryId { get; set; }
        public int OrderDetailId { get; set; }

        [Write(false)]
        public Delivery Delivery { get; set; }

        [Write(false)]
        public OrderDetail OrderDetail { get; set; }
    }
}