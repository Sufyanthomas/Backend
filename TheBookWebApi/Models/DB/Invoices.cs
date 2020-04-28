using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Invoices
    {
        public Invoices()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int InvoId { get; set; }
        public int? InvoNo { get; set; }
        public int? CustId { get; set; }
        public int? SalesId { get; set; }
        public int? Amount { get; set; }
        public int? AmountDue { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsCommited { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
