using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoices>();
        }

        public int CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Mobile { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
