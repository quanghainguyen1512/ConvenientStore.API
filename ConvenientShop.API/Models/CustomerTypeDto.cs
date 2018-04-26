using System.Collections.Generic;
using System.Linq;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Models
{
    public class CustomerTypeDto
    {
        public string Name { get; set; }
        public IEnumerable<CustomerForTypeDto> Customers { get; set; }
        public int NumberOfCustomers => Customers.Count();
    }
}