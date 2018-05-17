using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("supplier")]
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Write(false)]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}