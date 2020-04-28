using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CostPrice { get; set; }
        public int? TaxId { get; set; }
        public int? Price { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsCommited { get; set; }

        public virtual Tax Tax { get; set; }
    }
}
