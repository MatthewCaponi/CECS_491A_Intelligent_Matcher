CREATE PROCEDURE [dbo].[Claim_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [type], [value], [isDefault]
	from dbo.[Claim];
end