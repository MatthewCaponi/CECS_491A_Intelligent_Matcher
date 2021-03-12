CREATE PROCEDURE [dbo].[UserProfile_Create]
	@FirstName nvarchar(50),
	@Surname nvarchar(50),
	@DateOfBirth date,
	@UserAccountId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserProfile]([FirstName], [Surname], [DateOfBirth], [UserAccountId])
                        values (@FirstName, @Surname, @DateOfBirth, @UserAccountId); 

	set @Id = SCOPE_IDENTITY();
end