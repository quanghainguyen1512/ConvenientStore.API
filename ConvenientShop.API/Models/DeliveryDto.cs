using System;

namespace ConvenientShop.API.Models
{
    public class DeliveryDto
    {
        public int DeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Cost { get; set; }
    }
}