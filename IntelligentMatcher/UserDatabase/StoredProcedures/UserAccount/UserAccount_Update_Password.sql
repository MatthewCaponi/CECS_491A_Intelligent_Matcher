CREATE PROCEDURE [dbo].[UserAccount_Update_Password]
	@Id int,
	@Password nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set Password = @Password
	where Id = @Id;
end
