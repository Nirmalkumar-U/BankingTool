SET IDENTITY_INSERT [users] ON

MERGE INTO [users] AS Target
USING (VALUES
  (1,N'cgV8Jbj/OtU0vVKhaH61/w==:F5Ou5JGycic=:g2W8uh3MPZddA5WR2Bu+Ig==',N'manager@gmail.com',N'Manager',N'Nirmal',1,4,1,'2025-02-21T18:44:39.100',N'Admin',NULL,NULL,0)
) AS Source ([UserId],[Password],[EmailId],[FirstName],[LastName],[City],[State],[IsActive],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
ON (Target.[UserId] = Source.[UserId])
WHEN MATCHED AND (
	NULLIF(Source.[Password], Target.[Password]) IS NOT NULL OR NULLIF(Target.[Password], Source.[Password]) IS NOT NULL OR 
	NULLIF(Source.[EmailId], Target.[EmailId]) IS NOT NULL OR NULLIF(Target.[EmailId], Source.[EmailId]) IS NOT NULL OR 
	NULLIF(Source.[FirstName], Target.[FirstName]) IS NOT NULL OR NULLIF(Target.[FirstName], Source.[FirstName]) IS NOT NULL OR 
	NULLIF(Source.[LastName], Target.[LastName]) IS NOT NULL OR NULLIF(Target.[LastName], Source.[LastName]) IS NOT NULL OR 
	NULLIF(Source.[City], Target.[City]) IS NOT NULL OR NULLIF(Target.[City], Source.[City]) IS NOT NULL OR 
	NULLIF(Source.[State], Target.[State]) IS NOT NULL OR NULLIF(Target.[State], Source.[State]) IS NOT NULL OR 
	NULLIF(Source.[IsActive], Target.[IsActive]) IS NOT NULL OR NULLIF(Target.[IsActive], Source.[IsActive]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[IsDeleted], Target.[IsDeleted]) IS NOT NULL OR NULLIF(Target.[IsDeleted], Source.[IsDeleted]) IS NOT NULL) THEN
 UPDATE SET
  [Password] = Source.[Password], 
  [EmailId] = Source.[EmailId], 
  [FirstName] = Source.[FirstName], 
  [LastName] = Source.[LastName], 
  [City] = Source.[City], 
  [State] = Source.[State], 
  [IsActive] = Source.[IsActive], 
  [CreatedDate] = Source.[CreatedDate], 
  [CreatedBy] = Source.[CreatedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [IsDeleted] = Source.[IsDeleted]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserId],[Password],[EmailId],[FirstName],[LastName],[City],[State],[IsActive],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
 VALUES(Source.[UserId],Source.[Password],Source.[EmailId],Source.[FirstName],Source.[LastName],Source.[City],Source.[State],Source.[IsActive],Source.[CreatedDate],Source.[CreatedBy],Source.[ModifiedDate],Source.[ModifiedBy],Source.[IsDeleted])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [users]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[users] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [users] OFF
GO

INSERT INTO [dbo].[UserRole]([RoleId],[UserId]) VALUES(1,1)
GO