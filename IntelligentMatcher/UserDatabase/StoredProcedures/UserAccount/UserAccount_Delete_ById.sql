CREATE PROCEDURE [dbo].[UserAccount_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[UserAccount]
	where Id = @Id;

end