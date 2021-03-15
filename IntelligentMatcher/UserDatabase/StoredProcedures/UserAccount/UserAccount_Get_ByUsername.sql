CREATE PROCEDURE [dbo].[UserAccount_Get_ByUsername]
	@Username nvarchar(50)
AS
begin

	set nocount on

	select [Id], [Username], [Password], [Salt], [EmailAddress],
	[AccountType], [AccountStatus], [CreationDate], [UpdationDate]
	from dbo.[UserAccount]
	where Username = @Username;
end