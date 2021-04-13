CREATE PROCEDURE [dbo].[FriendBlockList_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendBlockList] 
	where Id = @Id;
end