﻿CREATE PROCEDURE [dbo].[FriendsList_GetAll_ByUserId]
	@UserId int

AS
begin
	set nocount on;
	SELECT [Id], [User1Id], [User2Id], [Date]
	from dbo.FriendsList
	WHERE User1Id = @UserId OR User2Id = @UserId;
end
