CREATE PROCEDURE dbo.[AccountVerification_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.AccountVerification
	where Id = @Id;

end