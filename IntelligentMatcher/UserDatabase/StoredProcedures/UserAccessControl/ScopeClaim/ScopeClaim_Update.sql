CREATE PROCEDURE [dbo].[ScopeClaim_Update]
	@Id int,
	@scopeId int,
	@claimId int
AS
begin
	set nocount on;

	update dbo.[ScopeClaim]
	set scopeId = @scopeId,
	claimId = @claimId
	where Id = @Id;
end
