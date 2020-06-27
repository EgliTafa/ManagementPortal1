using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManagmentPortal_0_3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ManagmentPortal_0_3.Models
{
    public class ManagmentPortal_0_3Context : IdentityDbContext<IdentityUser>
    {
        public ManagmentPortal_0_3Context (DbContextOptions<ManagmentPortal_0_3Context> options)
            : base(options)
        {
        }

        public DbSet<ManagmentPortal_0_3.Models.Warehouse> Warehouse { get; set; }

        public DbSet<ManagmentPortal_0_3.Models.Sector> Sector { get; set; }

        public DbSet<ManagmentPortal_0_3.Models.Manager> Manager { get; set; }
    }
}
