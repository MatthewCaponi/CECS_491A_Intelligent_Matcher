CREATE PROCEDURE [dbo].[UserAccountSettings_Create]
	@FontSize int,
	@FontStyle nvarchar(50),
	@ThemeColor nvarchar(50),
	@UserId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserAccountSettings]([FontSize], [FontStyle], [ThemeColor], [UserId] )
                        values (@FontSize, @FontStyle, @ThemeColor, @UserId); 

	set @Id = SCOPE_IDENTITY();
end