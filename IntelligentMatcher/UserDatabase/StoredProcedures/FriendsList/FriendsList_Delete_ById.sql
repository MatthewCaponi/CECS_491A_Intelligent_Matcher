CREATE PROCEDURE [dbo].[FriendsList_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendsList] 
	where Id = @Id;
end