using System;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("delivery")]
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int SupplierId { get; set; }
        public int Cost { get; set; }

        [Write(false)]
        public Supplier Supplier { get; set; }
    }
}