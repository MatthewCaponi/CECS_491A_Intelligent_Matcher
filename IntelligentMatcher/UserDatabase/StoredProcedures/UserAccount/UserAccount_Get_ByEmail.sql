CREATE PROCEDURE [dbo].[UserAccount_Get_ByEmail]
	@EmailAddress nvarchar(50)
AS
begin

	set nocount on

	select [Id], [Username], [Password], [Salt], [EmailAddress],
	[AccountType], [AccountStatus], [CreationDate], [UpdationDate], [StatusToken]
	from dbo.[UserAccount]
	where EmailAddress = @EmailAddress;
end