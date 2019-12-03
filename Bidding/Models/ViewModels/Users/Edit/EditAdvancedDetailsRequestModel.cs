namespace Bidding.Models.ViewModels.Users.Edit
{
    public class EditAdvancedDetailsRequestModel : EditBasicDetailsRequestModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string SubscriptionTill { get; set; }
    }
}
