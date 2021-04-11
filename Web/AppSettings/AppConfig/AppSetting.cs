using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RapidFireLib.Lib.Core;
using RapidFireLib.View.Models.Identity;
using RapidFireLib.View.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Config
{
    public class AppSetting : IAppSetting
    {
        private Tuple<ApplicationUser,IList<string>,IList<string>> GetRoles()
        {
            var _httpContextAccessor = new HttpContextAccessor();
            var userManager = (UserManager<ApplicationUser>)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
            var signInManager = (SignInManager<ApplicationUser>)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(SignInManager<ApplicationUser>));
            var roleManager = (RoleManager<ApplicationRole>)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(RoleManager<ApplicationRole>));
            var x = userManager.GetUserAsync(signInManager.Context.User).Result;
            if(x == null)
            {
                var userEmail = signInManager.Context.User.Claims.FirstOrDefault(y => y.Type.Contains("email")).Value;
                x = userManager.FindByEmailAsync(userEmail).Result;
            }
            var roleIds = userManager.GetRolesAsync(x).Result;
            var roles = roleManager.Roles.Where(y => roleIds.Contains(y.Id))?.Select(y=>y.Name).ToList();
            return new Tuple<ApplicationUser, IList<string>,IList<string>>(x,roles,roleIds);
        }
        public void ConfigureSetting(List<Setting> setting)
        {
            var currentUserRoles = GetRoles();
            var userInfo = currentUserRoles.Item1;
            var roleInfo = currentUserRoles.Item2;
            var releIds = currentUserRoles.Item3;

            if (userInfo.UserType != 99)
            {
                setting.Where(x => x.Id == 1).Select(x =>
                {
                    x.IsActive = false;
                    return x;
                }).ToList();
            }
            
            if (userInfo.UserType == 1 || userInfo.UserType == 99)
            {
                setting.Add(new Setting { Id = 20, ViewName = "ModelRegistration", ItemTitle = "Setup", ParentId = 0, DisplayOrder = 1, IsActive = true });
                setting.Add(new Setting { Id = 21, ViewName = "GradeInfo", Url = "/View/Settings/GradeInfo", ItemTitle = "Grade/Category", ParentId = 20, DisplayOrder = 21, IsActive = true });
            }
        }
    }
}
