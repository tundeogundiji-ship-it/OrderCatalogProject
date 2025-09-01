using ProductCatalog.Dormain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Dormain
{
    public class OrderItem:BaseDormainEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
    }
}
