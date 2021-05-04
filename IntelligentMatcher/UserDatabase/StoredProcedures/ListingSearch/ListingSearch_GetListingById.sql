CREATE PROCEDURE [dbo].[GetListingById]
  @Id int
AS
begin
	set nocount on;
	Select [Id],[Title],[Details],[City],[State],[NumberOfParticipants],
	[InPersonOrRemote],[UserAccountID]
	from dbo.Listing where Id=@Id;
end