CREATE PROCEDURE [dbo].[AccountVerification_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [UserId], [Token]
	from dbo.AccountVerification;
end
