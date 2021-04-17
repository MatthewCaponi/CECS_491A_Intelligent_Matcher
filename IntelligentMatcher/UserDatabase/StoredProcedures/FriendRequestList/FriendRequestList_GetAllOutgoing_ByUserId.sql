CREATE PROCEDURE [dbo].[FriendRequestList_GetAllOutgoing_ByUserId]
	@UserId int

AS
begin
	set nocount on;
	SELECT [Id], [User1Id], [User2Id], [Date]
	from dbo.FriendRequestList
	WHERE User2Id = @UserId;
end
