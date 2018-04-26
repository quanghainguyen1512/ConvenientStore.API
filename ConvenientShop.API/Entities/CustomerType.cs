using System.Collections.Generic;

namespace ConvenientShop.API.Entities
{
    public class CustomerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}