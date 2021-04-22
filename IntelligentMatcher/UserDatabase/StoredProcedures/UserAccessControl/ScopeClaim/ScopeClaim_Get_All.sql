CREATE PROCEDURE [dbo].[ScopeClaim_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [scopeId], [claimId]
	from dbo.[ScopeClaim];
end