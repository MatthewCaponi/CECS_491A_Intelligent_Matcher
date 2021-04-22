CREATE PROCEDURE [dbo].[ScopeClaim_Create]
	@scopeId int,
	@claimId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[ScopeClaim]([scopeId], [claimId])
		values (@scopeId, @claimId);

	set @Id = SCOPE_IDENTITY();
end