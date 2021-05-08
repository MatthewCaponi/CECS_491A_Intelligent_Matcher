CREATE PROCEDURE [dbo].[PageVisitTracker_Create]
	@Id int,
	@PageVisitedName nvarchar(50),
	@PageVisitTime datetime,
	@UserId int
AS
begin 
	set nocount on;
	insert into dbo.[PageVisitTracker]([PageVisitedName],[PageVisitTime],[UserId])
	values (@PageVisitedName,@PageVisitTime,@UserId);

	set @Id = SCOPE_IDENTITY();
end;