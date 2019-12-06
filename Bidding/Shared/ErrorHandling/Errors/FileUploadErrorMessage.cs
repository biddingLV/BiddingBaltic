using System.ComponentModel;

namespace Bidding.Shared.ErrorHandling.Errors
{
    public enum FileUploadErrorMessage
    {
        [Description("Missing file information")]
        MissingFileInformation,

        [Description("Could not upload - max file upload limit reached(30)")]
        MaxFileLimitReached,

        [Description("Could not upload - unsupported file type.")]
        UnsupportedFileType,

        [Description("Could not upload - file too large.")]
        FileTooLarge,

        [Description("Could not upload - file empty.")]
        FileEmpty,

        [Description("Could not upload - unsupported file extension.")]
        UnsupportedFileExtension,

        [Description("Can not upload files")]
        GenericUploadErrorMessage
    }
}
