using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcPicash.Models
{
    public class PaymentCommitment
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public float TotalAmmount { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Installment> Installments { get; set; }
        public PaymentcommitmentStatus PaymentcommitmentStatus { get; set; }

        public PaymentCommitment()
        {
        }
    }
}
