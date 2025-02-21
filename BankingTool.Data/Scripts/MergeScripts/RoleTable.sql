SET IDENTITY_INSERT [role] ON

MERGE INTO [role] AS Target
USING (VALUES
  (1,N'Manager',N'M0001',1,'2025-02-21T00:00:00',N'Admin',NULL,NULL,0)
 ,(2,N'Customer',N'C0001',4,'2025-02-21T00:00:00',N'Admin',NULL,NULL,0)
 ,(3,N'Staff',N'S0001',2,'2025-02-21T00:00:00',N'Admin',NULL,NULL,0)
) AS Source ([RoleId],[RoleName],[RoleCode],[RoleLevel],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
ON (Target.[RoleId] = Source.[RoleId])
WHEN MATCHED AND (
	NULLIF(Source.[RoleName], Target.[RoleName]) IS NOT NULL OR NULLIF(Target.[RoleName], Source.[RoleName]) IS NOT NULL OR 
	NULLIF(Source.[RoleCode], Target.[RoleCode]) IS NOT NULL OR NULLIF(Target.[RoleCode], Source.[RoleCode]) IS NOT NULL OR 
	NULLIF(Source.[RoleLevel], Target.[RoleLevel]) IS NOT NULL OR NULLIF(Target.[RoleLevel], Source.[RoleLevel]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[IsDeleted], Target.[IsDeleted]) IS NOT NULL OR NULLIF(Target.[IsDeleted], Source.[IsDeleted]) IS NOT NULL) THEN
 UPDATE SET
  [RoleName] = Source.[RoleName], 
  [RoleCode] = Source.[RoleCode], 
  [RoleLevel] = Source.[RoleLevel], 
  [CreatedDate] = Source.[CreatedDate], 
  [CreatedBy] = Source.[CreatedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [IsDeleted] = Source.[IsDeleted]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([RoleId],[RoleName],[RoleCode],[RoleLevel],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
 VALUES(Source.[RoleId],Source.[RoleName],Source.[RoleCode],Source.[RoleLevel],Source.[CreatedDate],Source.[CreatedBy],Source.[ModifiedDate],Source.[ModifiedBy],Source.[IsDeleted])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [role]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[role] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [role] OFF
GO