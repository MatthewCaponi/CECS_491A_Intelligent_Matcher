CREATE PROCEDURE [dbo].[Claim_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [name], [description], [isDefault]
	from dbo.[Claim];
end