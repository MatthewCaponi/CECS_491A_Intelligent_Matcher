CREATE PROCEDURE [dbo].[AccessPolicyPairing_Create]
	@scopeId int,
	@claimId int,
	@accessPolicyId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[AccessPolicyPairing]([scopeId], [claimId], [accessPolicyId])
		values (@scopeId, @claimId, @accessPolicyId);

	set @Id = SCOPE_IDENTITY();
end