CREATE PROCEDURE [dbo].[UserAccount_Update_Salt]
	@Id int,
	@Salt nvarchar(100)
AS
begin
	set nocount on;

	update dbo.[UserAccount]
	set Salt = @Salt
	where Id = @Id;
end
