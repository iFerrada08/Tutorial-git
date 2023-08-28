using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.MvcIdentity.Model
{
    public class ChinookUser : IdentityUser
    {
        public string Dni { get; set; }
    }
}
