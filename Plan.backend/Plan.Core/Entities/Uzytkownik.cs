using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Uzytkownik : IdentityUser
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public Uzytkownik()
        {
        }

        public Uzytkownik(string email) : base(email)
        {
            Email = email;
        }
    }
}
