using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Tax
    {
        public Tax()
        {
            Purchase = new HashSet<Purchase>();
            Sales = new HashSet<Sales>();
        }

        public int TaxId { get; set; }
        public int? TaxNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbrevation { get; set; }
        public int? TaxRate { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
