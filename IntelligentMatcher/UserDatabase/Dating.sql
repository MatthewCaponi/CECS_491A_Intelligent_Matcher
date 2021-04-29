CREATE TABLE [dbo].[Dating]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[SexualOrientationPreference] NVARCHAR(50) not null,
	[LookingFor] NVARCHAR(50) not null,
	[ListingId] INT NOT NULL,
)
