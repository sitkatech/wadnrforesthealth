IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pSetMeToWADNRAdminRole'))
DROP PROCEDURE dbo.pSetMeToWADNRAdminRole
GO

CREATE PROCEDURE dbo.pSetMeToWADNRAdminRole 
(
	@FirstName varchar(100),
	@LastName varchar(100)
)
AS  
begin  
	update dbo.Person 
	set RoleID = 8
	where FirstName = @FirstName 
	and LastName = @LastName
end
go
