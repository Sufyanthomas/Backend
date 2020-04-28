using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class Income
    {
        public int IncomeId { get; set; }
        public string Name { get; set; }
        public int? InvoId { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
        public int? Tax { get; set; }
        public int? TotalAmount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
