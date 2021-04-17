CREATE PROCEDURE [dbo].[Resource_Update]
	@Id int,
	@name nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[Resource]
	set name = @name
	where Id = @Id;
end
