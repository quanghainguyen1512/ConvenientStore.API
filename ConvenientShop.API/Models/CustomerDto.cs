namespace ConvenientShop.API.Models
{
    public class CustomerDto : CustomerSimpleDto
    {
        // public string FullName { get; set; }
        // public int Age { get; set; }
        // public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CustomerType { get; set; }
        public string Gender { get; set; }
    }
}