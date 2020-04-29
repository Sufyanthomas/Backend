using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Models.ViewModel
{
    public class SalesViewModel
    {
        [Required]
        public int SalesId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CostPrice { get; set; }

        public int TaxId { get; set; }

        public int Price { get; set; }

        public bool IsCommited { get; set; }
    }
}
