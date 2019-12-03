namespace Bidding.Shared.Constants
{
    public static class ApplicationUserRoles
    {
        public static readonly string BasicUser = "BasicUser";
        public static readonly string AuctionCreator = "AuctionCreator";
        public static readonly string PageAdministrator = "PageAdministrator";

        /// <summary>
        /// Never include this in the result set!
        /// </summary>
        public static readonly string SuperAdministrator = "SuperAdministrator";
    }
}
