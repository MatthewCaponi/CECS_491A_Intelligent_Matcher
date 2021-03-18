CREATE PROCEDURE [dbo].[Channel_Get_Id]
	@Id int
AS
begin

	set nocount on

	select [Id], [Name], [OwnerId]
	from dbo.[Channels]
	where Id = @Id;
end