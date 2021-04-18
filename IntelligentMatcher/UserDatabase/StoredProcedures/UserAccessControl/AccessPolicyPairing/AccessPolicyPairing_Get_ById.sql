CREATE PROCEDURE [dbo].[AccessPolicyPairing_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [scopeId], [claimId], [accessPolicyId]
	from dbo.[AccessPolicyPairing]
	where Id = @Id;
end