﻿CREATE PROCEDURE [dbo].[FriendsList_Delete]
	@UserId1 int,
	@UserId2 int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendsList] 
	where (User1Id = @UserId1 AND User2Id = @UserId2) OR (User1Id = @UserId2 AND User2Id = @UserId1);
end