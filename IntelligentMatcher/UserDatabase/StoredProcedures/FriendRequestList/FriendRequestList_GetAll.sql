﻿CREATE PROCEDURE [dbo].[FriendRequestList_GetAll]
AS
begin
	set nocount on;
	SELECT [Id], [User1Id], [User2Id], [Date]
	from dbo.FriendRequestList
end
