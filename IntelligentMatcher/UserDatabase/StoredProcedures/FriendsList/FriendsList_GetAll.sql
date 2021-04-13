CREATE PROCEDURE [dbo].[FriendsList_GetAll]
AS
begin
	set nocount on;
	SELECT [Id], [User1Id], [User2Id], [Date]
	from dbo.FriendsList
end
