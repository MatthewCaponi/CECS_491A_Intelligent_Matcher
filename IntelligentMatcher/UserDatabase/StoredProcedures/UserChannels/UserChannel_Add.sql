CREATE PROCEDURE [dbo].[UserChannel_Add]
	@ChannelId int,
	@UserId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserChannels]([ChannelId], [UserId] )
	values (@ChannelId, @UserId);

	set @Id = SCOPE_IDENTITY();
end