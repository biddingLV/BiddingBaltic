using Bidding.Database.Contexts;
using Bidding.Database.DatabaseModels;
using Bidding.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.SeedDemo
{
    public static class SeedApplicationData
    {
        /// <summary>
        /// This ensures there is a SuperAdmin user in the system.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task CheckAddSuperAdminAsync(this IServiceProvider serviceProvider)
        {
            //using (var scope = serviceProvider.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var extraContext = services.GetRequiredService<BiddingContext>();
            //    if (extraContext.UserToRoles.Any(x => x.RoleName == SuperAdminRoleName))
            //        //For security reasons there can only be one user with the SuperAdminRoleName
            //        return;

            //    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            //    var config = services.GetRequiredService<IConfiguration>();
            //    var superSection = config.GetSection("SuperAdmin");
            //    if (superSection == null)
            //        return;

            //    var userEmail = superSection["Email"];
            //    var userPassword = superSection["Password"];

            //    var superUser = await userManager.CheckAddNewUserAsync(userEmail, userPassword);

            //    using (var context = services.GetRequiredService<BiddingContext>())
            //    {
            //        var extraService = new ExtraAuthUsersSetup(context);
            //        extraService.AddUpdateRoleToPermissions(SuperAdminRoleName, "SuperAdmin Role", new List<Permissions> { Permissions.AccessAll });
            //        extraService.CheckAddRoleToUser(superUser.Id, SuperAdminRoleName);
            //        context.SaveChanges();
            //    }
            //}
        }
    }
}