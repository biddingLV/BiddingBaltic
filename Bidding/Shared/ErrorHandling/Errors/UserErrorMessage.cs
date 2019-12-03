using System.ComponentModel;

namespace Bidding.Shared.ErrorHandling.Errors
{
    public enum UserErrorMessage
    {
        [Description("Users information missing")]
        MissingUsersInformation,

        [Description("You can not sign-in")]
        CanNotSignIn,

        [Description("Please verify your e-mail")]
        UsersEmailNotVerified,

        [Description("Could not create the user")]
        CouldNotCreateUser,

        [Description("Could not update the user")]
        CouldNotUpdateUser,

        [Description("Please specify your e-mail!")]
        EmailNotSpecified,

        [Description("Not valid user!")]
        UserNotValid,

        [Description("User is not active anymore")]
        UserNotActive,

        [Description("Incorrect user role")]
        IncorrectUserRole,

        [Description("Could not subscribe to newsletter using email approach")]
        SubscribeEmailFails,

        [Description("Could not subscribe to newsletter using whatsapp approach")]
        SubscribWhatsAppFails,

        [Description("Could not fetch user list")]
        CouldNotFetchUserList,
    }
}
