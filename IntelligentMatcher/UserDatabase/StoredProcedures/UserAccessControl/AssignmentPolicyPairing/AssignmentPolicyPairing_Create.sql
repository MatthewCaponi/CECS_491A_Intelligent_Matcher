CREATE PROCEDURE [dbo].[AssignmentPolicyPairing_Create]
	@policyId int,
	@scopeId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[AssignmentPolicyPairing]([policyId], [scopeId])
		values (@policyId, @scopeId);

	set @Id = SCOPE_IDENTITY();
end