using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Bill
    {
        public Bill()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int BillId { get; set; }
        public int? BillNo { get; set; }
        public int? SupplierId { get; set; }
        public int? PurchaseId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? AmountDue { get; set; }
        public int? Amount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Status { get; set; }
        public bool? IsCommited { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
