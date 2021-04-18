CREATE PROCEDURE [dbo].[AccessPolicy_Create]
	@name nvarchar(50),
	@resourceId int,
	@priority int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[AccessPolicy]([name], [resourceId], [priority])
		values (@name, @resourceId, @priority);

	set @Id = SCOPE_IDENTITY();
end