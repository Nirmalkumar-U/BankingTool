SET IDENTITY_INSERT [State] ON

MERGE INTO [State] AS Target
USING (VALUES
  (1,N'Andhra Pradesh',N'AP','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(2,N'Karnataka',N'KA','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(3,N'Maharashtra',N'MH','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(4,N'Tamil Nadu',N'TN','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(5,N'Uttar Pradesh',N'UP','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(6,N'West Bengal',N'WB','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(7,N'Gujarat',N'GJ','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(8,N'Rajasthan',N'RJ','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(9,N'Kerala',N'KL','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(10,N'Madhya Pradesh',N'MP','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(11,N'Punjab',N'PB','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(12,N'Odisha',N'OD','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(13,N'Telangana',N'TG','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
 ,(14,N'Uttarakhand',N'UK','2024-12-31T16:34:20.450',N'System',NULL,NULL,0)
) AS Source ([StateId],[StateName],[Abbreviation],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
ON (Target.[StateId] = Source.[StateId])
WHEN MATCHED AND (
	NULLIF(Source.[StateName], Target.[StateName]) IS NOT NULL OR NULLIF(Target.[StateName], Source.[StateName]) IS NOT NULL OR 
	NULLIF(Source.[Abbreviation], Target.[Abbreviation]) IS NOT NULL OR NULLIF(Target.[Abbreviation], Source.[Abbreviation]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[IsDeleted], Target.[IsDeleted]) IS NOT NULL OR NULLIF(Target.[IsDeleted], Source.[IsDeleted]) IS NOT NULL) THEN
 UPDATE SET
  [StateName] = Source.[StateName], 
  [Abbreviation] = Source.[Abbreviation], 
  [CreatedDate] = Source.[CreatedDate], 
  [CreatedBy] = Source.[CreatedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [IsDeleted] = Source.[IsDeleted]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([StateId],[StateName],[Abbreviation],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
 VALUES(Source.[StateId],Source.[StateName],Source.[Abbreviation],Source.[CreatedDate],Source.[CreatedBy],Source.[ModifiedDate],Source.[ModifiedBy],Source.[IsDeleted])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [State]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[State] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [State] OFF
GO