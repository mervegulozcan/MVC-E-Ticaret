using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje_eticaret.App_Classes
{
    public class UserClass
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
    }
}