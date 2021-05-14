CREATE PROCEDURE [dbo].[UserScope_Create]
	@Type nvarchar(50),
	@UserAccountId int,
	@ID int output
AS
begin
	set nocount on;

	insert into dbo.[UserScope]([Type], [UserAccountId])
		values (@Type, @UserAccountId);

	set @Id = SCOPE_IDENTITY();
end