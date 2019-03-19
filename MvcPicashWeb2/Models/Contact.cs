using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcPicash.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public ContactType ContactType { get; set; }
        public string PersonId { get; set; }
        

        public Contact()
        {
        }
    }
}
