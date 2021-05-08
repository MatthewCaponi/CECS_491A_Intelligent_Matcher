CREATE PROCEDURE [dbo].[DeleteTeam]
		@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[TeamModel]
	where Id = @Id;

end
