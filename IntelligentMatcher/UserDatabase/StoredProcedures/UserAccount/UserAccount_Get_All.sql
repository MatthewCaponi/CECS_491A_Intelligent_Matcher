CREATE PROCEDURE [dbo].[UserAccount_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [Username], [Password], [Salt], [EmailAddress], [AccountType], [AccountStatus], [CreationDate], [UpdationDate], [StatusToken]
	from dbo.UserAccount;
end
