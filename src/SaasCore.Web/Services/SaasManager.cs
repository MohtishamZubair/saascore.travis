using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;
using SaasCore.Web.Data;
using SaasCore.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SaasCore.Web.Services
{
   public class SaasManager : ISaasManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext context;

        public SaasManager(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
        ApplicationDbContext _dbContext
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            context = _dbContext;
        }
        

        public async Task<ServiceResult> AddRoleAsync(string roleName)
        {
            bool isAdded = false;
            string result = "Role already Exists!";
            var roleExisted =await _roleManager.FindByNameAsync(roleName);
            if (roleExisted ==null )//   await _roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole newRole = new IdentityRole { Name = roleName };
                var resultRole = await GetRoleManager().CreateAsync(newRole);
                //GetRoleManager().Create(newRole);
                isAdded = resultRole.Succeeded;
                result = !isAdded ? string.Join(",", resultRole.Errors) : newRole.Id;
            }
            else
            {
                isAdded = true;
                result = roleExisted.Id;
            }

            ServiceResult serviceResult = new ServiceResult { isSuccess = isAdded, Message = result };
            return serviceResult;
        }

        private RoleManager<IdentityRole> GetRoleManager()
        {
            return _roleManager;
        }

        public async Task AddUserAndItsRole(string userName, string password, string roleName)
        {
            string roleResult = string.Empty;
            ServiceResult sericeResult = await AddRoleAsync(roleName);

            if (sericeResult.isSuccess)
            {
                roleResult = sericeResult.Message;
                ApplicationUser newUserResult = new ApplicationUser { UserName = string.Empty };

                var userAdd = await AddUserAsync(userName, password);
                string newUserId = string.Empty;
                if (userAdd != null && !(await GetUserManager().IsInRoleAsync(userAdd,roleName)))
                {
                  newUserId = newUserResult.Id;
                  await  AddRolesToUser(userAdd, new[] { roleResult });
                }
            }            
        }

        private async Task<ApplicationUser> AddUserAsync(string userName, string password)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(userName);

            if (appUser == null)
            {
                var user = new ApplicationUser { UserName = userName, Email =userName };
                await  GetUserManager().CreateAsync(user, password);
                appUser = user;
            }            
                       
            return appUser;
        }

        private UserManager<ApplicationUser> GetUserManager()
        {
            return _userManager;
        }

        private async Task<IdentityResult> AddRolesToUser(ApplicationUser decrypted, string[] newRoles)
        {
            var manager = GetUserManager();
            return await manager.AddToRolesAsync(decrypted, context.Roles.Where(r => newRoles.Contains(r.Id)).Select(r => r.Name).ToArray());
        }

        /*
        public ServiceResult AddUserAndItsRoles(NewUserWithRolesId newUserWithIds)
        {
            ServiceResult sr = new ServiceResult();
            sr.isSuccess = false;
            sr.Message = "User can not be added successfully";

            ApplicationUser user = new ApplicationUser { UserName = newUserWithIds.Email, Email = newUserWithIds.Email, PhoneNumber = newUserWithIds.ContactNumber, PhoneNumberConfirmed = true, EmailConfirmed = true };
            var manager = GetUserManager();
            var result = manager.Create(user, newUserWithIds.ContactNumber);
            sr.isSuccess = result.Succeeded;
            sr.Message = !sr.isSuccess ? string.Join(",", result.Errors) : user.Id;

            var roleResult = AddRolesToUserId(user.Id, newUserWithIds.RoleIds);

            return sr;
        }

        public ServiceResult UpdateUserAndItsRolesAndApps(string decrypted, string email, string contactNumber, string[] newRoles, string[] oldRoles, int[] apps)
        {
            ServiceResult sr = new ServiceResult();
            sr.isSuccess = false;
            sr.Message = "User can not be added successfully";

            var manager = GetUserManager();
            var user = manager.FindById(decrypted);
            user.Email = email;
            user.PasswordHash = manager.PasswordHasher.HashPassword(contactNumber);
            user.UserName = email;
            user.PhoneNumber = contactNumber;
            var result = manager.Update(user);

            sr.isSuccess = result.Succeeded;
            sr.Message = !sr.isSuccess ? string.Join(",", result.Errors) : decrypted;

            if ((oldRoles != null && oldRoles.Length > 0) && (newRoles != null && newRoles.Length > 0))
            {
                manager.RemoveFromRoles(decrypted, context.Roles.Where(r => oldRoles.Contains(r.Id)).Select(r => r.Name).ToArray());
            }
            if (newRoles != null && newRoles.Length > 0)
                AddRolesToUserId(decrypted, newRoles);
            if (user.SaasApps.Count > 0 && (apps != null && apps.Length > 0))
            {
                user.SaasApps.Clear();
            }
            AddAppsToUser(apps, user);
            return sr;
        }

        

        public bool EditRole(IdentityRole role, out string result)
        {
            bool isAdded = false;
            result = "Role does not exists!";
            if (!context.Roles.Any(r => r.Id == role.Id))
            {
                var resultRole = GetRoleManager().Update(role);
                isAdded = resultRole.Succeeded;
                result = !isAdded ? string.Join(",", resultRole.Errors) : role.Id;
            }
            return isAdded;
        }

        public ServiceResult EditRole(IdentityRole role)
        {
            ServiceResult sr = new ServiceResult();
            sr.isSuccess = false;
            sr.Message = "Role does not exists!";
            if (context.Roles.Any(r => r.Id == role.Id))
            {
                var resultRole = GetRoleManager().Update(role);
                sr.isSuccess = resultRole.Succeeded;
                sr.Message = !sr.isSuccess ? string.Join(",", resultRole.Errors) : role.Id;
            }
            return sr;
        }

        public ServiceResult EditApplication(SaasApp updateApp)
        {
            ServiceResult sr = new ServiceResult();
            sr.isSuccess = false;

            context.Entry(updateApp) =  //System.Data.Entity.EntityState.Modified;
            int count = context.SaveChanges();
            sr.isSuccess = count != -1;
            sr.Message = updateApp.SaasAppId.ToString();
            return sr;
        }

        public ServiceResult AddApplication(SaasApp model)
        {
            ServiceResult sr = new ServiceResult();
            sr.isSuccess = false;
            sr.Message = "Failure in saving!";
            var result = context.SaasApps.Add(model);
            int count = context.SaveChanges();
            sr.isSuccess = count != -1;
            sr.Message = result.SaasAppId.ToString();
            return sr;
        }
*/
        //public ServiceResult AddRole(IdentityRole newRole)
        //{
        //    ServiceResult sr = new ServiceResult();
        //    sr.isSuccess = false;
        //    sr.Message = "Role already Exists!";

        //    if (!context.Roles.Any(r => r.Name == newRole.Name))
        //    {
        //        var resultRole = GetRoleManager().Create(newRole);
        //        sr.isSuccess = resultRole.Succeeded;
        //        sr.Message = !sr.isSuccess ? string.Join(",", resultRole.Errors) : newRole.Id;
        //    }
        //    return sr;
        //}

        //public async Task<IdentityResult> DeleteRoleAsync(string id)
        //{
        //    var manager = GetRoleManager();
        //    var role = manager.FindById(id);
        //    return await manager.DeleteAsync(role);
        //}
/*
        public ServiceResult DeleteUser(string id)
        {
            var manager = GetUserManager();
            var user = manager.FindById(id);
            var result = manager.Delete(user);

            ServiceResult sr = new ServiceResult();
            sr.isSuccess = result.Succeeded;
            sr.Message = !sr.isSuccess ? string.Join(",", result.Errors) : id;

            return sr;
        }

        public void DeleteApplication(int id)
        {
            var app = context.SaasApps.Find(new object[] { id });
            app.IsActive = false;
            context.SaveChanges();

        }

        public ServiceResult AddUserAndItsRolesAndApps(string Email, string ContactNumber, string[] RoleIds, int[] AppIds)
        {
            ServiceResult sr = new ServiceResult();
            sr.isSuccess = false;
            sr.Message = "User can not be added successfully";

            ApplicationUser user = new ApplicationUser { UserName = Email, Email = Email, PhoneNumber = ContactNumber, PhoneNumberConfirmed = true, EmailConfirmed = true };
            var manager = GetUserManager();
            var result = manager.Create(user, ContactNumber);
            sr.isSuccess = result.Succeeded;
            sr.Message = !sr.isSuccess ? string.Join(",", result.Errors) : user.Id;

            if (RoleIds != null && RoleIds.Length > 0)
            {
                var roleResult = AddRolesToUserId(user.Id, RoleIds);
                sr.Message = sr.isSuccess && !roleResult.Succeeded ? string.Join(",", result.Errors) : sr.Message;
                sr.isSuccess = sr.isSuccess ? roleResult.Succeeded : sr.isSuccess;
            }
            AddAppsToUser(AppIds, user);
            return sr;
        }

        private void AddAppsToUser(int[] AppIds, ApplicationUser user)
        {
            if (AppIds != null && AppIds.Length > 0)
            {
                context.SaasApps.Where(app => AppIds.Contains(app.SaasAppId)).ToList().ForEach(app => user.SaasApps.Add(app));
                context.SaveChanges();
                //var appResult =  AddAppsToUserId(user.Id, AppIds);
            }
        }

        private int AddAppsToUserId(string id, int[] appIds)
        {
            return -1;
        }*/
    }
}