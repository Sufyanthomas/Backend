using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Models.ViewModel.ExpAndInc
{
    public class IncomeViewModel
    {
        [Required]
        public int IncomeId { get; set; }

        public string Name { get; set; }

        public int InvoId { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public int Tax { get; set; }

        public int TotalAmount { get; set; }
    }
}
