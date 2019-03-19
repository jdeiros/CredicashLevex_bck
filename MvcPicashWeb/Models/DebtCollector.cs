using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcPicash.Models
{
    public class DebtCollector
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime Birthdate { get; set; }
        public string CellPhone { get; set; }
        public string OptionalContact { get; set; }

        public List<Route> Routes { get; set; }

        public DebtCollector()
        {
        }
    }
}
