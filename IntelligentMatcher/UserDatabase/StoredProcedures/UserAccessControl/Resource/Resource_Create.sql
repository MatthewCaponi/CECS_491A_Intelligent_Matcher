CREATE PROCEDURE [dbo].[Resource_Create]
	@name nvarchar(50),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Resource]([name])
		values (@name);

	set @Id = SCOPE_IDENTITY();
end