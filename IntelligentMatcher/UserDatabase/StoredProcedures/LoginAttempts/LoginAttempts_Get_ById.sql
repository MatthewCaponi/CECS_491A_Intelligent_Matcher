CREATE PROCEDURE [dbo].[LoginAttempts_Get_ById]
	@Id int
AS
begin

	set nocount on

	select [Id], [IpAddress], [LoginCounter], [SuspensionEndTime]
	from dbo.[LoginAttempts]
	where Id = @Id;
end