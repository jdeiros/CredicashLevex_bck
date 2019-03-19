using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcPicash.Models
{
    public class Route
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DebtCollectorId { get; set; }
        public DebtCollector DebtCollector { get; set; }
        public List<Customer> Customers { get; set; }

        public Route()
        {
        }
    }
}
