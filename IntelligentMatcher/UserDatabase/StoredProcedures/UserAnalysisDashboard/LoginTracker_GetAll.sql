CREATE PROCEDURE [dbo].[LoginTracker_GetAll]
AS
begin
	set nocount on;
	Select [Id],[Username],[LoginTime]
	from dbo.LoginTracker;
end

