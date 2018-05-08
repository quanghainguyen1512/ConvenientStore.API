using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("bill")]
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public Staff Staff { get; set; }
        public DateTime CreatedDateTime { get; set; }
        // public Customer Customer { get; set; }
        [Write(false)]
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}