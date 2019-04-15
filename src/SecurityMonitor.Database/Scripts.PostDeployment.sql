/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [Devices] t
USING (
SELECT 0 AS [DeviceId],'Vicarage Rd' AS [Name],-0.401486 AS [Longitude],51.649836 AS [Latitude]
UNION ALL
SELECT 1,'Liberty',-3.9351,51.6422
UNION ALL
SELECT 2,'St Marys',-1.391111,50.905833
UNION ALL
SELECT 3,'Madejski',-0.982778,51.422222
UNION ALL
SELECT 4,'Villa Park',-1.884722,52.509167
) s
ON ( t.[DeviceId]= s.[DeviceId] )
  WHEN NOT MATCHED THEN
     INSERT( [DeviceId], [Name], [Longitude], [Latitude])
     VALUES( s.[DeviceId], s.[Name], s.[Longitude], s.[Latitude])
;
GO

MERGE INTO [DeviceEvents] t
USING (
SELECT '83653235-aacf-4cff-a55a-98f9005f865c' AS [EventId], 0 AS [DeviceId],'2019-04-15' AS [EventTime]
UNION ALL
SELECT '088e1385-857c-49cf-a824-17925e8b6a7d',1,'2019-04-15'
UNION ALL
SELECT 'ef0b01dd-2a2f-4182-81bf-839c950999ec',2,'2019-04-15'
UNION ALL
SELECT '11554e9a-f69e-4f23-a1da-146e635b9564',3,'2019-04-15'
UNION ALL
SELECT '8e9c1c68-da6e-41e8-916f-06bb322951c0',4,'2019-04-15'
) s
ON ( t.[EventId]= s.[EventId] )
  WHEN NOT MATCHED THEN
     INSERT( [EventId], [DeviceId], [EventTime], [Status])
     VALUES( s.[EventId], s.[DeviceId], s.[EventTime], 0)
;

