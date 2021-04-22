CREATE PROCEDURE [dbo].[UserAccount_Get_ById]
	@Id int
AS
begin

	set nocount on

	select [Id], [Username], [Password], [Salt], [EmailAddress],
	[AccountType], [AccountStatus], [CreationDate], [UpdationDate], [StatusToken]
	from dbo.[UserAccount]
	where Id = @Id;
end