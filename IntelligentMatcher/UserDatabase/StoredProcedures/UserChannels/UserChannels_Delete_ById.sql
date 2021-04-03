CREATE PROCEDURE [dbo].[UserChannels_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[UserChannels]
	where Id = @Id;

end