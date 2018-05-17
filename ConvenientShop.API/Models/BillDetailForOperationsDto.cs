namespace ConvenientShop.API.Models
{
    public class BillDetailForOperationsDto
    {
        public string BarCode { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int BillId { get; set; }
    }
}