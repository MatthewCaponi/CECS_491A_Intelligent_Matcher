CREATE PROCEDURE [dbo].[UserAccount_Update_Email]
	@Id int,
	@EmailAddress nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set EmailAddress = @EmailAddress
	where Id = @Id;
end
