CREATE PROCEDURE [dbo].[AccessPolicy_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [name], [resourceId], [priority]
	from dbo.[AccessPolicy]
	where Id = @Id;
end