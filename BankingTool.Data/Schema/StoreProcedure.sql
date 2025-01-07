IF OBJECT_ID('GetActionsByUserId', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE GetActionsByUserId;
END
GO

CREATE PROCEDURE GetActionsByUserId
    @UserId INT
AS
BEGIN

SELECT a.ActionId,a.ActionName,a.ActionPath,a.ActionType,a.MenuLevel,a.ParrentMenuId,a.Sequence
FROM Users u
JOIN UserRole ur ON u.UserId = ur.UserId
JOIN Role r ON ur.RoleId = r.RoleId
JOIN RoleAccess ra ON r.RoleId = ra.RoleId
JOIN Action a ON ra.ActionId = a.ActionId
WHERE u.UserId = @UserId
ORDER BY a.ParrentMenuId, a.Sequence

END
GO
------------------------------------------------------------------------------------------------------