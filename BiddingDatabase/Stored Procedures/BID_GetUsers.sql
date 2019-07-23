CREATE PROCEDURE [dbo].[BID_GetUsers] @start int,
@end int
AS
BEGIN
	SELECT
		usr.UserId,
		usr.FirstName,
		usr.MiddleName,
		usr.LastName,
		usr.ContactEmail,
		usr.CreatedAt,
		rol.Name AS UserRole,
		(CASE
			WHEN usr.Deleted = 0 THEN 'Active'
			ELSE 'Inactive'
		END) AS UserStatus
	FROM Users usr
	INNER JOIN Roles rol ON usr.RoleId = rol.RoleId
	ORDER BY usr.FirstName
  OFFSET @start ROWS
  FETCH NEXT @end ROWS ONLY
END