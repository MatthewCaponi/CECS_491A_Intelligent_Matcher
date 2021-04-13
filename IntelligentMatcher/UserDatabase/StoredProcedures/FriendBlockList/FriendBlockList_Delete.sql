CREATE PROCEDURE [dbo].[FriendBlockList_Delete]
	@UserId1 int,
	@UserId2 int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendBlockList] 
	where User1Id = @UserId1 AND User2Id = @UserId2;
end