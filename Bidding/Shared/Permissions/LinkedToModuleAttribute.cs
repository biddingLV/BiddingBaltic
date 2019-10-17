using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Permissions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LinkedToModuleAttribute : Attribute
    {
        public PaidForModules PaidForModule { get; private set; }

        public LinkedToModuleAttribute(PaidForModules paidForModule)
        {
            PaidForModule = paidForModule;
        }
    }
}
