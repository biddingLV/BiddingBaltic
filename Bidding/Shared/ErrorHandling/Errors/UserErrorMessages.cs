using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.ErrorHandling.Errors
{
    public enum UserErrorMessages
    {
        [Description("You can not sign-in.")]
        CanNotSignIn,

        [Description("Please verify your e-mail.")]
        UsersEmailNotVerified,

        [Description("Could not create the user.")]
        CouldNotCreateUser,

        [Description("Please specify your e-mail!")]
        EmailNotSpecified,

        [Description("Not valid user!")]
        UserNotValid,
    }
}
