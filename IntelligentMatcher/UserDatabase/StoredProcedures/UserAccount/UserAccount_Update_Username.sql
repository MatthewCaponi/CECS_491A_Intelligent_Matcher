CREATE PROCEDURE [dbo].[UserAccount_Update_Username]
	@Id int,
	@Username nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set Username = @Username
	where Id = @Id;
end
