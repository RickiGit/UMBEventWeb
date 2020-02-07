using EventApps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Models
{
    public partial class UserAccess
    {
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}