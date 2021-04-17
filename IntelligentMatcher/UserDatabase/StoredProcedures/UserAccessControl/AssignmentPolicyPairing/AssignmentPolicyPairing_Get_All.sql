CREATE PROCEDURE [dbo].[AssignmentPolicyPairing_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [policyId], [scopeId]
	from dbo.[AssignmentPolicyPairing];
end