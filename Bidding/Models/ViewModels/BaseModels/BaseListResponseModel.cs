namespace Bidding.Models.ViewModels.BaseModels
{
    public abstract class BaseListResponseModel
    {
        public int CurrentPage { get; set; }
        public int ItemCount { get; set; }
        public int Offset { get; set; }
        public int TotalPages { get; set; }
        public abstract bool IsReponseEmpty();
    }
}
