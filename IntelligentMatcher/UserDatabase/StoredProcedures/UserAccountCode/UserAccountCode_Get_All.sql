CREATE PROCEDURE [dbo].[UserAccountCode_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [Code], [ExpirationTime], [UserAccountId]
	from dbo.UserAccountCode;
end