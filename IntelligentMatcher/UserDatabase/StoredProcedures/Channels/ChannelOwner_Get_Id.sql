CREATE PROCEDURE [dbo].[ChannelOwner_Get_Id]
	@Id int
AS
begin

	set nocount on

	select [OwnerId]
	from dbo.[Channels]
	where Id = @Id;
end