using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.ViewModels.BaseModels
{
    public class BaseListRequestModel
    {
        // for example, name, price
        public string SortByColumn { get; set; }

        // for example, asc or desc
        public string SortingDirection { get; set; }

        // for example, magic
        public string SearchValue { get; set; }

        // start from 0 or 10, 100
        public int OffsetStart { get; set; }

        // take only till 10, 20, 100
        public int OffsetEnd { get; set; }

        public int CurrentPage { get; set; }
    }
}