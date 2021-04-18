CREATE PROCEDURE [dbo].[AssignmentPolicyPairing_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [policyId], [scopeId]
	from dbo.[AssignmentPolicyPairing]
	where Id = @Id;
end