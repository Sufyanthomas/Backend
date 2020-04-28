using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Models.ViewModel
{
    public class InvoicesViewModel
    {
        [Required]
        public int InvoId { get; set; }

        public int InvoNo { get; set; }

        public int CustId { get; set; }

        public int SalesId { get; set; }

        public int Amount { get; set; }

        public int AmountDue { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool IsCommited { get; set; }
    }
}
