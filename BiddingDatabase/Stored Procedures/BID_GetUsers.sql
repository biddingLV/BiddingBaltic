CREATE PROCEDURE [dbo].[BID_GetUsers] 
	@start int,
	@end int,
	@userId int
AS
BEGIN
	SELECT
		usr.Id as UserId,
		usr.FirstName,
		usr.LastName,
		usr.Email,
		usr.PhoneNumber as Phone,
		rol.Name as RoleName
	FROM AspNetUsers usr
	INNER JOIN AspNetUserRoles urol ON usr.Id = urol.UserId
	INNER JOIN AspNetRoles rol ON urol.RoleId = rol.Id
	WHERE usr.Id != @userId
	ORDER BY usr.Id DESC
	OFFSET @start ROWS
	FETCH NEXT @end ROWS ONLY
END