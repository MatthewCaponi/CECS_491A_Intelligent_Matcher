CREATE PROCEDURE [dbo].[AccountVerification_Create.sql]
	@Token NVARCHAR(200),
	@UserId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[AccountVerification]([UserId], [Token])
	values (@UserId, @Token);

	set @Id = SCOPE_IDENTITY();
end