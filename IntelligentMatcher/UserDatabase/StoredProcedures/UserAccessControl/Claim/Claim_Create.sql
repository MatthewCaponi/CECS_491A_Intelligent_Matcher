﻿CREATE PROCEDURE [dbo].[Claim_Create]
	@name nvarchar(50),
	@description nvarchar(200),
	@isDefault bit,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Claim]([name], [description], [isDefault])
		values (@name, @description, @isDefault);

	set @Id = SCOPE_IDENTITY();
end