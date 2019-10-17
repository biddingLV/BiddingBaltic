using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Permissions
{
    public static class DataAuthConstants
    {
        public const int HierarchicalKeySize = 64;
        public const int AccessKeySize = 64;

        public const string HierarchicalKeyClaimName = "DataKey";
    }
}
