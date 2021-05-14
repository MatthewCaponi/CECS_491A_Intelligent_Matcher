CREATE PROCEDURE [dbo].[UserClaim_Delete_ByUserClaimId]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[UserClaim]
	where Id = @Id;

end