using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SaasCore.Web.Helper;
using SaasCore.Web.Models;
using SaasCore.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaasCore.Web.Data
{
    public static class DataInitializer
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            var userManager = serviceProvider.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
            var dbContext = serviceProvider.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

           await new SaasManager(userManager,roleManager,dbContext).AddUserAndItsRole(AppConstant.ADMIN_USER_NAME, AppConstant.ADMIN_USER_PASSWORD, AppConstant.ADMIN_ROLE_NAME);

            //string role = AppConstant.ADMIN_ROLE_NAME;
            //    if (!await roleManager.RoleExistsAsync(role))
            //    {
            //        await roleManager.CreateAsync(new IdentityRole(role));
            //    }
            
        }
    }
}
