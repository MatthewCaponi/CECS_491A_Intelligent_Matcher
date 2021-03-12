CREATE PROCEDURE [dbo].[UserAccount_GetSalt_ById]
	@Id int
AS
begin

	set nocount on

	select [Salt]
	from dbo.[UserAccount]
	where Id = @Id;
end