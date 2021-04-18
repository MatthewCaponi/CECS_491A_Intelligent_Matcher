CREATE PROCEDURE [dbo].[AssignmentPolicyPairing_Update]
	@Id int,
	@policyId int,
	@scopeId int
AS
begin
	set nocount on;

	update dbo.[AssignmentPolicyPairing]
	set policyId = @policyId,
	scopeId = @scopeId
	where Id = @Id;
end
