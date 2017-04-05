using System.Threading.Tasks;
using Models;

namespace SaasCore.Web.Services
{
    public interface ISaasManager
    {
        Task<ServiceResult> AddRoleAsync(string roleName);
        Task AddUserAndItsRole(string userName, string password, string roleName);
    }
}