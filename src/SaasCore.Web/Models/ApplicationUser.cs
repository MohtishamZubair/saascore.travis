using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace SaasCore.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
/*            if (SaasApps == null)
            {
                SaasApps = new List<SaasApp>();
            }*/
        }
        public List<SaasAppUser> SaasAppUsers { get; set; }
    }
}
