CREATE PROCEDURE [dbo].[Channels_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [Name], [OwnerId]
	from dbo.Channels;
end
