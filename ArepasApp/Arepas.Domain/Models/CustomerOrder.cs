using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arepas.Domain.Models
{
    public class CustomerOrder
    {
        public Customer Customer { get; set; } = null!;

        public IEnumerable<Order> Orders { get; set; } = null!;
    }
}