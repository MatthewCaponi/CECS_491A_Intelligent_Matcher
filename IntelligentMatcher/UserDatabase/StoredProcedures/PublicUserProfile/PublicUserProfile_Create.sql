CREATE PROCEDURE [dbo].[PublicUserProfile_Create]
	@UserId INT,
	@Description NVARCHAR(1000),
	@Intrests NVARCHAR(1000),
	@Hobbies NVARCHAR(1000),
	@Jobs NVARCHAR(1000),
	@Goals NVARCHAR(1000),
	@Age INT,
	@Gender NVARCHAR(6),
	@Ethnicity NVARCHAR(100),
	@SexualOrientation NVARCHAR(100),
	@Height NVARCHAR(1000),
	@Visibility NVARCHAR(20),
	@Status NVARCHAR(20),
	@Photo NVARCHAR(1000),
	@Id int output

AS
begin
	set nocount on;

	insert into dbo.[PublicUserProfile]([UserId], [Description], [Intrests], [Hobbies], [Jobs], [Goals], [Age], [Gender], [Ethnicity], [SexualOrientation], [Height], [Visibility], [Status], [Photo])
	values (@UserId, @Description, @Intrests, @Hobbies, @Jobs, @Goals, @Age, @Gender, @Ethnicity, @SexualOrientation, @Height, @Visibility, @Status, @Photo);

	set @Id = SCOPE_IDENTITY();
end