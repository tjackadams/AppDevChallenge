CREATE PROCEDURE [dbo].[usp_InsertOrUpdateDevice]
@Id INT,
@Name nvarchar(200),
@Latitude float,
@Longitude float
AS

SET NOCOUNT ON;

MERGE [dbo].[Devices] AS target
USING(SELECT @Id, @Name, @Latitude, @Longitude) AS source (Id,[Name],Latitude,Longitude)
ON (target.DeviceId = source.Id)
WHEN MATCHED THEN 
	UPDATE SET 
		TARGET.Name = SOURCE.[Name], 
		TARGET.Latitude = SOURCE.Latitude, 
		TARGET.Longitude = SOURCE.Longitude
WHEN NOT MATCHED THEN 
INSERT (DeviceId,[Name],Latitude,Longitude)
VALUES(SOURCE.Id, SOURCE.[Name], SOURCE.Latitude, SOURCE.Longitude);

SET NOCOUNT OFF;
