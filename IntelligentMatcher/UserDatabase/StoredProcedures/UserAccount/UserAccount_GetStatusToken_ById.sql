CREATE PROCEDURE [dbo].[UserAccount_GetStatusToken_ById]
	@Id int
AS
begin

	set nocount on

	select [StatusToken]
	from dbo.[UserAccount]
	where Id = @Id;
end