CREATE PROCEDURE [dbo].[Scope_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [type], [description], [isDefault]
	from dbo.[Scope]
	where Id = @Id;
end