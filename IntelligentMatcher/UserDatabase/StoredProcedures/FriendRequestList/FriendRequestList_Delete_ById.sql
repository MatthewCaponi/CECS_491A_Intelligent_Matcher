﻿CREATE PROCEDURE [dbo].[FriendRequestList_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[FriendRequestList] 
	where Id = @Id;
end