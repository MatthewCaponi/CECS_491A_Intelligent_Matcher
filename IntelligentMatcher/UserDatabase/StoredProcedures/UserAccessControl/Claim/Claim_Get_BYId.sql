CREATE PROCEDURE [dbo].[Claim_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [type], [value], [isDefault]
	from dbo.[Claim]
	where Id = @Id;
end