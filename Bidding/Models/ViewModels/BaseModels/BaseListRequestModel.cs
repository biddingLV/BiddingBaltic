namespace Bidding.Models.ViewModels.BaseModels
{
    public class BaseListRequestModel
    {
        /// <summary>
        /// For example, name, price
        /// </summary>
        public string SortByColumn { get; set; }

        /// <summary>
        /// For example, asc or desc
        /// </summary>
        public string SortingDirection { get; set; }

        /// <summary>
        /// For example, magic, auto
        /// </summary>
        public string SearchValue { get; set; }

        /// <summary>
        /// Start from 0 or 10, 100 and so on
        /// </summary>
        public int OffsetStart { get; set; }

        /// <summary>
        /// Take only till 10, 20, 100
        /// </summary>
        public int OffsetEnd { get; set; }

        public int CurrentPage { get; set; }
    }
}