CREATE PROCEDURE [dbo].[FriendRequestsList_GetAll_ByUserId]
	@UserId int

AS
begin
	set nocount on;
	SELECT [Id], [User1Id], [User2Id], [Date]
	from dbo.FriendsRequestsList
	WHERE User1Id = @UserId;
end
