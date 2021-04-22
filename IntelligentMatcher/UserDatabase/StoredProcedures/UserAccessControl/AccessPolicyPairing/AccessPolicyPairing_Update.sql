CREATE PROCEDURE [dbo].[AccessPolicyPairing_Update]
	@Id int,
	@scopeId int,
	@claimId int,
	@accessPolicyId int
AS
begin
	set nocount on;

	update dbo.[AccessPolicyPairing]
	set scopeId = @scopeId,
	claimId = @claimId,
	accessPolicyId = @accessPolicyId
	where Id = @Id;
end
