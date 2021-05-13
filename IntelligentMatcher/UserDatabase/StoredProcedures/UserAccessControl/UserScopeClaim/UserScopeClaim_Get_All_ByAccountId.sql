﻿CREATE PROCEDURE [dbo].[UserScopeClaim_Get_All_ByAccountId]
	@accountId int
AS
begin
	set nocount on;
	SELECT [Id], [userAccountId], [scopeClaimId], [role]
	from dbo.[UserScopeClaim]
	where userAccountId = @accountId;
end