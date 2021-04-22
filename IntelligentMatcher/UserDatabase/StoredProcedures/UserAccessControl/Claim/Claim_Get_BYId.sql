CREATE PROCEDURE [dbo].[Claim_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [name], [description], [isDefault]
	from dbo.[Claim]
	where Id = @Id;
end