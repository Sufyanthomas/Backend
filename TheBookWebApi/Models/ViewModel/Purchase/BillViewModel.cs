using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Models.ViewModel.Purchase
{
    public class BillViewModel
    {
        [Required]
        public int BillId { get; set; }

        public int BillNo { get; set; }

        public int SupplierId { get; set; }

        public int PurchaseId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int AmountDue { get; set; }

        public int Amount { get; set; }

        public string Status { get; set; }

        public bool IsCommited { get; set; }
    }
}
