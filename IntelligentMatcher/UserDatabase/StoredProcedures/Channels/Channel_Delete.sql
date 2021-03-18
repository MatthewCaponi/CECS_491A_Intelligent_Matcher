CREATE PROCEDURE [dbo].[Channel_Delete]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[Channels]
	where Id = @Id;

end