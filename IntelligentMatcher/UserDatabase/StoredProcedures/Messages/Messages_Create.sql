CREATE PROCEDURE [dbo].[Messages_Create]
	@ChannelId int,
	@ChannelMessageId int,
	@UserId int,
	@Message nvarchar(1000),
    @Date date,
	@Time NVARCHAR(100),
	@Username NVARCHAR(100),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Messages]([ChannelId], [ChannelMessageId], [UserId], [Message],[Date], [Time], [Username])
	values (@ChannelId, @ChannelMessageId, @UserId, @Message, @Date, @Time, @Username);

	set @Id = SCOPE_IDENTITY();
end