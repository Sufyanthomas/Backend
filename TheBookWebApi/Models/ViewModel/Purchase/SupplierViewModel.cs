using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Models.ViewModel.Purchase
{
    public class SupplierViewModel
    {
        [Required]
        public int SupplierId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Mobile { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

    }
}
