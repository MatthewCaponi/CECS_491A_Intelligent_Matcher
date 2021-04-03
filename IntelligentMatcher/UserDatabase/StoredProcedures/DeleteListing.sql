CREATE PROCEDURE [dbo].[DeleteListing]
AS
begin
	set nocount on;
	Delete  from dbo.[Listing];
	
end


