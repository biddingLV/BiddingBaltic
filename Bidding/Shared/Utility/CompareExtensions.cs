using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Utility
{
    public static class CompareExtensions
    {
        public static bool IsEmpty(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty || guid == default(Guid);
        }
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty || guid == default(Guid);
        }

        public static bool IsEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsEmpty(this int? number)
        {
            return number == null || !number.HasValue;
        }

        //public static bool IsEmpty(this int number)
        //{
        //    return;
        //}

        public static bool IsEmpty(this DateTime date)
        {
            return date == DateTime.MinValue;
        }

        public static bool IsEmpty(this DateTime? date)
        {
            return date == null || date == DateTime.MinValue;
        }

        public static bool IsEmpty(this object customObject)
        {
            return customObject != default(object);
        }
    }
}
