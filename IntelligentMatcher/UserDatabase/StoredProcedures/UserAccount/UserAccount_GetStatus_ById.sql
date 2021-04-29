CREATE PROCEDURE [dbo].[UserAccount_GetStatus_ById]
	@Id int
AS
begin

	set nocount on

	select [AccountStatus]
	from dbo.[UserAccount]
	where Id = @Id;
end