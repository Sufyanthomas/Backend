using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Models.ViewModel
{
    public class CustomerViewModel
    {
        [Required]
        public int CustId { get; set; }

        [Required]
        [StringLength (50, ErrorMessage = "max. 50 characters ")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public int Mobile { get; set; }

        public string Address { get; set; }

        public bool? IsActive { get; set; }
    }
}
