using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.ErrorHandling.Errors
{
    public enum FileUploadErrorMessages
    {
        [Description("Missing file information")]
        MissingFileInformation,

        [Description("Could not upload, because max image upload limit reached(30)")]
        MaxImageLimitReached,
    }
}
