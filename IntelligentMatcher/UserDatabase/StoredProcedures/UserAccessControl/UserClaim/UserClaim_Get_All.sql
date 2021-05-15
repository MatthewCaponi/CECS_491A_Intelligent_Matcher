CREATE PROCEDURE [dbo].[UserClaim_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [Type], [Value], [UserAccountId]
	from dbo.[UserClaim];
end