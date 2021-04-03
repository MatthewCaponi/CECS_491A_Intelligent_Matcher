CREATE PROCEDURE [dbo].[UserAccountCode_Get_ByUserAccountId]
	@UserAccountId int
AS
begin

	set nocount on

	select [Id], [Code], [ExpirationTime], [UserAccountId]
	from dbo.[UserAccountCode]
	where UserAccountId = @UserAccountId;
end