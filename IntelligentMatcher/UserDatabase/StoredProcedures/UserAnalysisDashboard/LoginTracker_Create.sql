CREATE PROCEDURE [dbo].[LoginTracker_Create]
	@Id int,
	@Username nvarchar(50),
	@LoginTime datetime
AS
begin 
	set nocount on;
	insert into dbo.[LoginTracker]([Username],[LoginTime])
	values (@Username,@LoginTime);

	set @Id = SCOPE_IDENTITY();
end;