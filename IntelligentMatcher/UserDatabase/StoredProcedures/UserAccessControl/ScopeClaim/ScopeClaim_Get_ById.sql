CREATE PROCEDURE [dbo].[ScopeClaim_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [scopeId], [claimId]
	from dbo.[ScopeClaim]
	where Id = @Id;
end