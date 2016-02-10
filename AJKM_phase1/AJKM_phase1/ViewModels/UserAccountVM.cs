using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AJKM_phase1.ViewModels
{
    public class UserAccountVM
    {
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}