CREATE PROCEDURE [dbo].[Resource_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [name]
	from dbo.[Resource]
	where Id = @Id;
end