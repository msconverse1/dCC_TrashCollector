using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSC_dCC_TrashCollector.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MSC_dCC_TrashCollector.Context
{
    public class ApplicationDataContext : IdentityDbContext<AppUser>
    {
        public ApplicationDataContext()
            : base("DefaultConnection")
        { }

        public System.Data.Entity.DbSet<AppUser> AppUsers { get; set; }
    }
}