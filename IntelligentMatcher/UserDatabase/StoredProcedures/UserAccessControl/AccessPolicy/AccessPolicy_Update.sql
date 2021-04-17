CREATE PROCEDURE [dbo].[AccessPolicy_Update]
	@Id int,
	@name nvarchar(50),
	@resourceId int,
	@priority int
AS
begin
	set nocount on;

	update dbo.[AccessPolicy]
	set name = @name,
	resourceId = @resourceId,
	priority = @priority
	where Id = @Id;
end
