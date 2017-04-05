using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaasCore.Web.Models;

namespace SaasCore.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<SaasApp> SaasApps { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<SaasAppUser>()
                .HasKey(k => new { k.ApplicationUserId, k.SaasAppId });

            builder.Entity<SaasAppUser>()
                .HasOne(k => k.SaasApp)
                .WithMany(a => a.SaasAppUsers)
                .HasForeignKey(k => k.SaasAppId);

            builder.Entity<SaasAppUser>()
                .HasOne(k => k.ApplicationUser)
                .WithMany(au => au.SaasAppUsers)
                .HasForeignKey(k => k.ApplicationUserId);


        }
    }
}
