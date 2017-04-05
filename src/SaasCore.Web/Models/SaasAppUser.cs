
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaasCore.Web.Models
{
    using SaasCore.Web.Models;

    public class SaasAppUser
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int SaasAppId { get; set; }
        public SaasApp SaasApp { get; set; }
    }
}
