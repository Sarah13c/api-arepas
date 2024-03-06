using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arepas.Domain.Models
{
    public class OrderOrderDetail
    {
        public Order Order { get; set; } = null!;
        public IEnumerable<OrderDetailProduct> DetailProducts { get; set; } = null!;
    }
}