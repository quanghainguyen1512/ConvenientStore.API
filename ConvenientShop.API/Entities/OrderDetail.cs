using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("order_detail")]
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int ProductQuantity { get; set; }
        public bool IsReceived { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        [Write(false)]
        public Product Product { get; set; }

        [Write(false)]
        public Order Order { get; set; }
    }
}