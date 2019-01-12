using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Utility
{
    public static class ValidateInputs
    {
        public static bool IsNotSpecified(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty || guid == default(Guid);
        }

        public static bool IsNotSpecified(this Guid guid)
        {
            return guid == Guid.Empty || guid == default(Guid);
        }

        public static bool IsNotSpecified<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsNotSpecified(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotSpecified(this int? number)
        {
            return number == null || !number.HasValue;
        }

        public static bool IsNotSpecified(this int number)
        {
            return number <= 0;
        }

        public static bool IsNotSpecified(this DateTime date)
        {
            return date == DateTime.MinValue;
        }

        public static bool IsNotSpecified(this DateTime? date)
        {
            return date == null || date == DateTime.MinValue;
        }

        public static bool IsNotSpecified(this object customObject)
        {
            return customObject == default(object) || customObject == null;
        }
    }
}
