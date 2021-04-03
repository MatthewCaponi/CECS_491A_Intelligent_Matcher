CREATE PROCEDURE [dbo].[Channels_Create]
	@OwnerId int,
	@Name NVARCHAR(100),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Channels]([OwnerId], [Name])
	values (@OwnerId, @Name);

	set @Id = SCOPE_IDENTITY();
end