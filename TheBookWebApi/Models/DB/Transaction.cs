using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int? InvoId { get; set; }
        public int? BillId { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? Amount { get; set; }
        public int? TotalAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? IsCommited { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Invoices Invo { get; set; }
    }
}
