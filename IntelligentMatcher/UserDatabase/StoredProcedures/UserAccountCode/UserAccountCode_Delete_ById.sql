CREATE PROCEDURE [dbo].[UserAccountCode_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[UserAccountCode]
	where Id = @Id;

end