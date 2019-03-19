using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcPicash.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsMain { get; set; }

        public Address()
        {
        }
    }
}
