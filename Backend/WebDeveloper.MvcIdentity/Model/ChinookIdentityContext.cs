using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.MvcIdentity.Model
{
    public class ChinookIdentityContext : IdentityDbContext<ChinookUser>
    {
        public ChinookIdentityContext(DbContextOptions<ChinookIdentityContext> options)
            : base(options)
        {
        }
    }
}
