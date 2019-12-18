using Bidding.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureAuthorize
{
    /// <summary>
    /// This is the code that calculates what feature permissions a user has
    /// </summary>
    public class CalcAllowedPermissions
    {
        private readonly BiddingContext _context;

        public CalcAllowedPermissions(BiddingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is called if the Permissions that a user needs calculating.
        /// It looks at what permissions the user has, and then filters out any permissions
        /// they aren't allowed because they haven't get access to the module that permission is linked to.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>a string containing the packed permissions</returns>
        public async Task<string> CalcPermissionsForUserAsync(int userId)
        {
            var permissionsForUser =
                await (from urol in _context.UserRoles
                       join rol in _context.Roles on urol.RoleId equals rol.Id
                       join rperm in _context.RolesToPermissions on rol.Name equals rperm.RoleName
                       where urol.UserId == userId
                       select rperm.PermissionsInRole).FirstOrDefaultAsync().ConfigureAwait(true);

            //// we get the modules this user is allowed to see
            //// todo: KKE: Research this part!
            //var userModules = PaidForModules.None;// _context.ModulesForUsers.Find(userId)?.AllowedPaidForModules ?? PaidForModules.None;

            //// Now we remove permissions that are linked to modules that the user has no access to
            //var filteredPermissions =
            //    from permission in permissionsForUser
            //    let moduleAttr = typeof(Permission).GetMember(permission.ToString())[0]
            //        .GetCustomAttribute<LinkedToModuleAttribute>()
            //    where moduleAttr == null || userModules.HasFlag(moduleAttr.PaidForModule)
            //    select permission;

            return permissionsForUser; //.PackPermissionsIntoString();
        }
    }
}