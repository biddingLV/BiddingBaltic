using System;
using System.Collections.Generic;

namespace Bidding.Shared.Utility.Validation.Comparers
{
    public static class ValidateSpecifiedInputs
    {
        public static bool IsSpecified(this Guid? guid)
        {
            return guid != null || guid != Guid.Empty || guid != default(Guid);
        }

        public static bool IsSpecified(this Guid guid)
        {
            return guid != Guid.Empty || guid != default;
        }

        public static bool IsSpecified<T>(this List<T> list)
        {
            return list != null || list.Count != 0;
        }

        public static bool IsSpecified(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static bool IsSpecified(this int? number)
        {
            return number != null || number.HasValue;
        }

        public static bool IsSpecified(this int number)
        {
            return number > 0;
        }

        //public static bool IsSpecified(this DateTime date)
        //{
        //    return date == DateTime.MinValue;
        //}

        //public static bool IsSpecified(this DateTime? date)
        //{
        //    return date == null || date == DateTime.MinValue;
        //}

        public static bool IsSpecified(this object customObject)
        {
            return customObject != default || customObject != null;
        }
    }
}
