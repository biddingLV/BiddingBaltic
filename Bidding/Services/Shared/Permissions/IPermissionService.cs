namespace Bidding.Services.Shared.Permissions
{
    public interface IPermissionService
    {
        string GetUserId();
        int GetAndValidateUserId();
    }
}
