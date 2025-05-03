SET IDENTITY_INSERT [Action] ON

MERGE INTO [Action] AS Target
USING (VALUES
  (1,N'User',NULL,N'Menu',NULL,1,NULL,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(2,N'User List',N'/userList',N'Menu',N'R',2,1,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(3,N'User List',N'/userList',N'Menu',N'RW',2,1,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(4,N'Add Edit User',N'/addEditUser',N'Menu',N'R',2,1,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(5,N'Add Edit User',N'/addEditUser',N'Menu',N'RW',2,1,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(6,N'Transaction',NULL,N'Menu',NULL,1,NULL,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(7,N'Create Account',N'/createAccount',N'Menu',N'R',2,6,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(8,N'Create Account',N'/createAccount',N'Menu',N'RW',2,6,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(9,N'Transactions List',N'/transactions',N'Menu',N'R',2,6,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(10,N'Transactions List',N'/transactions',N'Menu',N'RW',2,6,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(11,N'Transfer Amount',N'/transfer',N'Menu',N'R',2,6,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(12,N'Transfer Amount',N'/transfer',N'Menu',N'RW',2,6,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(13,N'Self Transfer',N'/selfTransfer',N'Menu',N'R',2,6,4,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(14,N'Self Transfer',N'/selfTransfer',N'Menu',N'RW',2,6,4,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
) AS Source ([ActionId],[ActionName],[ActionPath],[ActionType],[Access],[MenuLevel],[ParrentMenuId],[Sequence],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
ON (Target.[ActionId] = Source.[ActionId])
WHEN MATCHED AND (
	NULLIF(Source.[ActionName], Target.[ActionName]) IS NOT NULL OR NULLIF(Target.[ActionName], Source.[ActionName]) IS NOT NULL OR 
	NULLIF(Source.[ActionPath], Target.[ActionPath]) IS NOT NULL OR NULLIF(Target.[ActionPath], Source.[ActionPath]) IS NOT NULL OR 
	NULLIF(Source.[ActionType], Target.[ActionType]) IS NOT NULL OR NULLIF(Target.[ActionType], Source.[ActionType]) IS NOT NULL OR 
	NULLIF(Source.[Access], Target.[Access]) IS NOT NULL OR NULLIF(Target.[Access], Source.[Access]) IS NOT NULL OR 
	NULLIF(Source.[MenuLevel], Target.[MenuLevel]) IS NOT NULL OR NULLIF(Target.[MenuLevel], Source.[MenuLevel]) IS NOT NULL OR 
	NULLIF(Source.[ParrentMenuId], Target.[ParrentMenuId]) IS NOT NULL OR NULLIF(Target.[ParrentMenuId], Source.[ParrentMenuId]) IS NOT NULL OR 
	NULLIF(Source.[Sequence], Target.[Sequence]) IS NOT NULL OR NULLIF(Target.[Sequence], Source.[Sequence]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[IsDeleted], Target.[IsDeleted]) IS NOT NULL OR NULLIF(Target.[IsDeleted], Source.[IsDeleted]) IS NOT NULL) THEN
 UPDATE SET
  [ActionName] = Source.[ActionName], 
  [ActionPath] = Source.[ActionPath], 
  [ActionType] = Source.[ActionType], 
  [Access] = Source.[Access], 
  [MenuLevel] = Source.[MenuLevel], 
  [ParrentMenuId] = Source.[ParrentMenuId], 
  [Sequence] = Source.[Sequence], 
  [CreatedDate] = Source.[CreatedDate], 
  [CreatedBy] = Source.[CreatedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [IsDeleted] = Source.[IsDeleted]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ActionId],[ActionName],[ActionPath],[ActionType],[Access],[MenuLevel],[ParrentMenuId],[Sequence],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[IsDeleted])
 VALUES(Source.[ActionId],Source.[ActionName],Source.[ActionPath],Source.[ActionType],Source.[Access],Source.[MenuLevel],Source.[ParrentMenuId],Source.[Sequence],Source.[CreatedDate],Source.[CreatedBy],Source.[ModifiedDate],Source.[ModifiedBy],Source.[IsDeleted])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Action]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Action] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Action] OFF
GO

INSERT INTO RoleAccess(RoleId,ActionId)
Values (1,1),
(1,3),
(1,5),
(1,6),
(1,8),
(2,6),
(2,10),
(2,12),
(2,14)