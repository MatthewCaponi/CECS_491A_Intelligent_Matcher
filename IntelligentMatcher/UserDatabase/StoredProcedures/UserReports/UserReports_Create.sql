CREATE PROCEDURE [dbo].[UserReports_Create]
	@Report NVARCHAR(1000),
	@ReportingId int,
	@ReportedId int,

    @Date date,
	@Id int output

AS
begin
	set nocount on;

	insert into dbo.[UserReports]([Report], [Date], [ReportingId], [ReportedId])
	values (@Report, @Date, @ReportingId, @ReportedId);

	set @Id = SCOPE_IDENTITY();
end