CREATE PROCEDURE [dbo].[FriendRequestsList_Delete]
	@UserId1 int,
	@UserId2 int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendsRequestsList] 
	where (User1Id = @UserId1 AND User2Id = @UserId2) OR (User1Id = @UserId2 AND User2Id = @UserId1);
end