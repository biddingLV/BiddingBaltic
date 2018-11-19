using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.ViewModels.BaseModels
{
    public class BaseListResponseModel
    {
        public int CurrentPage { get; set; }
        public int ItemCount { get; set; }
    }
}
