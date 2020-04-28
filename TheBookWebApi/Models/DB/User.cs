using System;
using System.Collections.Generic;

namespace TheBookWebApi.Models.DB
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
    }
}
