CREATE PROCEDURE [dbo].[UserAccountCode_Get_ById]
	@Id int
AS
begin

	set nocount on

	select [Id], [Code], [ExpirationTime], [UserAccountId]
	from dbo.[UserAccountCode]
	where Id = @Id;
end