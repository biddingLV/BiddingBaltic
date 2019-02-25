using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Bidding.Shared.ErrorHandling.Errors
{
    public static class EnumHelper
    {
        private static string GetDescriptionFromName(Type type, string name)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                return attribute?.Description ?? name.Replace("_", " ");
            }
            return name.Replace("_", " ");
        }

        /// <summary>
        /// Returns the description from the Description attribute of the sepcified enum value
        /// </summary>
        public static string GetDescriptionFromValue<T>(T value) where T : struct
        {
            return GetDescriptionFromName(typeof(T), value.ToString());
        }
    }
}
