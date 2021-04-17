CREATE PROCEDURE [dbo].[FriendBlockList_GetBlockingUser_ByUserId]
	@UserId int

AS
begin
	set nocount on;
	SELECT [Id], [User1Id], [User2Id], [Date]
	from dbo.FriendBlockList
	WHERE User2Id = @UserId ;
end
