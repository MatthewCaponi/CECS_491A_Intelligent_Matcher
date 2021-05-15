CREATE PROCEDURE [dbo].[UserClaim_Get_ByUserClaimId]
	@claimId int
AS
begin
	set nocount on;
	SELECT [Id], [Type], [Value], [UserAccountId]
	from dbo.[UserClaim]
	where Id = @claimId
end