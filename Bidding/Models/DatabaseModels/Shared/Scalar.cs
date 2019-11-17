using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.DatabaseModels.Shared
{
    /// <summary>
    /// Provides the result of stored procedures returning a scalar value in Entity Framework
    /// </summary>
    public class Scalar<T>
    {
        [Key]
        public T Value { get; private set; }
    }
}
