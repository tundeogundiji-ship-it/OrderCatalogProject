using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Models
{
    public class OrderResponse
    {
        public string? message { get; set; }
        public Guid orderId { get; set; }
    }
}
