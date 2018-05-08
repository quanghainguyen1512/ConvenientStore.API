using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("customer_type")]
    public class CustomerType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Write(false)]
        public ICollection<Customer> Customers { get; set; }
    }
}