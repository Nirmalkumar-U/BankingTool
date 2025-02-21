SET IDENTITY_INSERT [City] ON

MERGE INTO [City] AS Target
USING (VALUES
  (1,4,N'Coimbatore',N'641001','2024-12-31T00:00:00',N'System',NULL,NULL,0),
  (2,1,N'Tirupati',N'517501','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(3,1,N'Kurnool',N'518001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(4,1,N'Guntur',N'522002','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(5,1,N'Rajahmundry',N'533101','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(6,2,N'Mangaluru',N'575001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(7,2,N'Hubballi',N'580020','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(8,2,N'Belagavi',N'590001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(9,2,N'Kalaburagi',N'585101','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(10,3,N'Nagpur',N'440001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(11,3,N'Nashik',N'422001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(12,3,N'Aurangabad',N'431001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(13,3,N'Solapur',N'413001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(14,4,N'Madurai',N'625001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(15,4,N'Tiruchirappalli',N'620001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(16,4,N'Salem',N'636001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(17,4,N'Erode',N'638001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(18,5,N'Kanpur',N'208001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(19,5,N'Agra',N'282001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(20,5,N'Meerut',N'250001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(21,5,N'Noida',N'201301','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(22,6,N'Siliguri',N'734003','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(23,6,N'Asansol',N'713301','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(24,6,N'Durgapur',N'713203','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(25,6,N'Howrah',N'711101','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(26,7,N'Vadodara',N'390001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(27,7,N'Rajkot',N'360001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(28,7,N'Bhavnagar',N'364001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(29,7,N'Junagadh',N'362001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(30,8,N'Udaipur',N'313001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(31,8,N'Kota',N'324001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(32,8,N'Ajmer',N'305001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(33,8,N'Bikaner',N'334001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(34,9,N'Kollam',N'691001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(35,9,N'Kozhikode',N'673001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(36,9,N'Alappuzha',N'688001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(37,9,N'Thrissur',N'680001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(38,10,N'Gwalior',N'474001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(39,10,N'Jabalpur',N'482001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(40,10,N'Ujjain',N'456001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(41,10,N'Ratlam',N'457001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(42,11,N'Jalandhar',N'144001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(43,11,N'Patiala',N'147001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(44,11,N'Bathinda',N'151001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(45,11,N'Mohali',N'140308','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(46,12,N'Rourkela',N'769001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(47,12,N'Puri',N'752001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(48,12,N'Sambalpur',N'768001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(49,12,N'Balasore',N'756001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(50,13,N'Karimnagar',N'505001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(51,13,N'Nizamabad',N'503001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(52,13,N'Khammam',N'507001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(53,13,N'Mahbubnagar',N'509001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(54,14,N'Nainital',N'263001','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(55,14,N'Rishikesh',N'249201','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(56,14,N'Haldwani',N'263139','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
 ,(57,14,N'Roorkee',N'247667','2024-12-31T16:34:20.480',N'System',NULL,NULL,0)
) AS Source ([CityId],[StateId],[CityName],[PostalCode],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
ON (Target.[CityId] = Source.[CityId])
WHEN MATCHED AND (
	NULLIF(Source.[StateId], Target.[StateId]) IS NOT NULL OR NULLIF(Target.[StateId], Source.[StateId]) IS NOT NULL OR 
	NULLIF(Source.[CityName], Target.[CityName]) IS NOT NULL OR NULLIF(Target.[CityName], Source.[CityName]) IS NOT NULL OR 
	NULLIF(Source.[PostalCode], Target.[PostalCode]) IS NOT NULL OR NULLIF(Target.[PostalCode], Source.[PostalCode]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[IsDeleted], Target.[IsDeleted]) IS NOT NULL OR NULLIF(Target.[IsDeleted], Source.[IsDeleted]) IS NOT NULL) THEN
 UPDATE SET
  [StateId] = Source.[StateId], 
  [CityName] = Source.[CityName], 
  [PostalCode] = Source.[PostalCode], 
  [CreatedDate] = Source.[CreatedDate], 
  [CreatedBy] = Source.[CreatedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [IsDeleted] = Source.[IsDeleted]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([CityId],[StateId],[CityName],[PostalCode],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
 VALUES(Source.[CityId],Source.[StateId],Source.[CityName],Source.[PostalCode],Source.[CreatedDate],Source.[CreatedBy],Source.[ModifiedDate],Source.[ModifiedBy],Source.[IsDeleted])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [City]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[City] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [City] OFF
GO