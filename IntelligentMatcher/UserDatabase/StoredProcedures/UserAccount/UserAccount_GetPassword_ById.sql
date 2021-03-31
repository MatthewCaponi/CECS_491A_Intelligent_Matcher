CREATE PROCEDURE [dbo].[UserAccount_GetPassword_ById]
	@Id int
AS
begin

	set nocount on

	select [Password]
	from dbo.[UserAccount]
	where Id = @Id;
end