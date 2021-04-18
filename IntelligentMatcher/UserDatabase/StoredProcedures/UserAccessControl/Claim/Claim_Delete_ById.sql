CREATE PROCEDURE [dbo].[Claim_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[Claim]
	where Id = @Id;

end