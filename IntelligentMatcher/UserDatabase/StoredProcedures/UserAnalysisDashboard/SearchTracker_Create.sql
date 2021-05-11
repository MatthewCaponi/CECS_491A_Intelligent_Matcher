CREATE PROCEDURE [dbo].[SearchTracker_Create]
	@Id int,
	@Search nvarchar(50),
	@SearchTime datetime,
	@UserId int
AS
begin 
	set nocount on;
	insert into dbo.[ListingSearchTracker]([Search],[SearchTime],[UserId])
	values (@Search,@SearchTime,@UserId);

	set @Id = SCOPE_IDENTITY();
end;