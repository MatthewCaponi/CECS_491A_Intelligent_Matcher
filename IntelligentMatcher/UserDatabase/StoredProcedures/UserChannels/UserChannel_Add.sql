CREATE PROCEDURE [dbo].[UserChannel_Add]
	@ChannelId int,
	@UserId int,
	@AccountStatus nvarchar(20),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserChannels]([ChannelId], [UserId], [AccountStatus] )
	values (@ChannelId, @UserId, @AccountStatus);

	set @Id = SCOPE_IDENTITY();
end