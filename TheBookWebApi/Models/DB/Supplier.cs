using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Supplier
    {
        public Supplier()
        {
            Bill = new HashSet<Bill>();
        }

        public int SupplierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Mobile { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Bill> Bill { get; set; }
    }
}
