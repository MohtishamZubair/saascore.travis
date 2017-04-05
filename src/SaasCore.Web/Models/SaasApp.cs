using SaasCore.Web.Models;
using System.Collections.Generic;

namespace SaasCore.Web.Models
{
    public class SaasApp
    {
        public int SaasAppId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }   
             
        public List<SaasAppUser> SaasAppUsers { get; set; }
    }
}