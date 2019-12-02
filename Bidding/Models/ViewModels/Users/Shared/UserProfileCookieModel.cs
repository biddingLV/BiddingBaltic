namespace Bidding.Models.ViewModels.Users.Shared
{
    public class UserProfileCookieModel
    {
        public int UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
    }
}
