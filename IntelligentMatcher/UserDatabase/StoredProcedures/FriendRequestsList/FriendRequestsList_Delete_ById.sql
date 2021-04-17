CREATE PROCEDURE [dbo].[FriendRequestsList_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendsRequestsList] 
	where Id = @Id;
end