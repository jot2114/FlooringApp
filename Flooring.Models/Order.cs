using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class Order
    {
        public DateTime OrderDate { get; set; }
        public Product ProductOrdered { get; set; }
        public State State { get; set; }

        public int OrderId { get; set; }
        public string CustomerName { get; set; }       
        public decimal Area { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal TaxCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Total {get; set; }
    }
}
