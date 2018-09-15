using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.ViewModels.BaseModels
{
    public class BaseListRequestModel
    {
        public string SortByColumn { get; set; }
        public string SortingDirection { get; set; }
        public string SearchValue { get; set; }
        public int OffsetStart { get; set; }
        public int OffsetEnd { get; set; }
        public bool? AllData { get; set; }
    }
}