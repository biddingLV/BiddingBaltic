using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Permissions
{
    public class CalcDataKey
    {
        private readonly ExtraAuthorizeDbContext _context;

        public CalcDataKey(ExtraAuthorizeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This looks for a DataKey for the current user, which can be missing
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The found data key, or random guid string to stop it matching anything</returns>
        public string CalcDataKeyForUser(string userId)
        {
            return _context.DataAccess.Where(x => x.UserId == userId)
                .Select(x => x.LinkedTenant.DataKey).SingleOrDefault()
                   //If no data key then set to random guid to stop it matching anything
                   ?? Guid.NewGuid().ToString("N");
        }
    }
}
