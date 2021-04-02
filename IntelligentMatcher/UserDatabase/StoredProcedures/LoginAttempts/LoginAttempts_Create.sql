CREATE PROCEDURE [dbo].[LoginAttempts_Create]
	@IpAddress nvarchar(50),
	@LoginCounter int,
	@SuspensionEndTime datetimeoffset(7),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[LoginAttempts]([IpAddress], [LoginCounter], [SuspensionEndTime])
	values (@IpAddress, @LoginCounter, @SuspensionEndTime);

	set @Id = SCOPE_IDENTITY();
end