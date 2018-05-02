using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("order_detail")]
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}