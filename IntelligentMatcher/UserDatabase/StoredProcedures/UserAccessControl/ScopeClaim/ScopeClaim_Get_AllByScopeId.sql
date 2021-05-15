CREATE PROCEDURE [dbo].[ScopeClaim_Get_AllByScopeId]
	@scopeId int
AS
begin
	set nocount on;
	SELECT [Id], [scopeId], [claimId]
	from dbo.[ScopeClaim]
	where scopeId = @scopeId;
end