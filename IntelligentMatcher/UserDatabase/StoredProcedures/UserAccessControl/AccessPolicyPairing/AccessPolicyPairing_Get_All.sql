CREATE PROCEDURE [dbo].[AccessPolicyPairing_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [scopeId], [claimId], [accessPolicyId]
	from dbo.[AccessPolicyPairing];
end