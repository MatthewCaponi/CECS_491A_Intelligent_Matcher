CREATE PROCEDURE [dbo].[PublicUserProfile_Get_All]
AS
begin
	set nocount on;
	SELECT [Id],[UserId], [Description], [Intrests], [Hobbies], [Jobs], [Goals], [Age], [Gender], [Ethnicity], [SexualOrientation], [Height], [Visibility], [Status], [Photo]
	from dbo.PublicUserProfile;
end
