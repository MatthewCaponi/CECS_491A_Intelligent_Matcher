CREATE PROCEDURE [dbo].[Add_Friend]
	@User1Id int,
	@User2Id int,
    @Date date,
	@Id int output

AS
begin
	set nocount on;

	insert into dbo.[FriendsList]([User1Id], [User2Id], [Date])
	values (@User1Id, @User2Id, @Date);

	set @Id = SCOPE_IDENTITY();
end