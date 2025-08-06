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
 ,(15,N'Deposit Amount',N'/depositAmount',N'Menu',N'R',2,6,5,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(16,N'Deposit Amount',N'/depositAmount',N'Menu',N'RW',2,6,5,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(17,N'Cash Withdraw',N'/cashWithdraw',N'Menu',N'R',2,6,6,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(18,N'Cash Withdraw',N'/cashWithdraw',N'Menu',N'RW',2,6,6,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(19,N'Budget',NULL,N'Menu',NULL,1,NULL,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(20,N'Create Budget Account',N'/createBudgetAccount',N'Menu',N'R',2,19,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(21,N'Create Budget Account',N'/createBudgetAccount',N'Menu',N'RW',2,19,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(22,N'Budget Transaction List',N'/budgetTransactionList',N'Action',N'R',NULL,19,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(23,N'Budget Transaction List',N'/budgetTransactionList',N'Action',N'RW',NULL,19,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(24,N'Notification',NULL,N'ActionMenu',NULL,1,NULL,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(25,N'Budget Notification',N'/budgetNotification',N'Action',N'R',2,24,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(26,N'Budget Notification',N'/budgetNotification',N'Action',N'RW',2,24,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(27,N'Account Notification',N'/accountNotification',N'Action',N'R',2,24,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(28,N'Account Notification',N'/accountNotification',N'Action',N'RW',2,24,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(29,N'Report',NULL,N'Menu',NULL,1,NULL,5,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(30,N'Account Statement Report',N'/accountStatementReport',N'Menu',N'R',2,29,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(31,N'Account Statement Report',N'/accountStatementReport',N'Menu',N'RW',2,29,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(32,N'Budget Statement Report',N'/budgetStatementReport',N'Menu',N'R',2,29,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(33,N'Budget Statement Report',N'/budgetStatementReport',N'Menu',N'RW',2,29,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(34,N'Budget List',N'/budgetList',N'Menu',N'R',2,19,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(35,N'Budget List',N'/budgetList',N'Menu',N'RW',2,19,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(36,N'Budget Transfer',N'/budgetTransfer',N'Menu',N'R',2,19,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(37,N'Budget Transfer',N'/budgetTransfer',N'Menu',N'RW',2,19,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(38,N'Calender',NULL,N'ActionMenu',NULL,1,NULL,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(39,N'Calender',N'/calender',N'Action',N'R',2,38,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(40,N'Calender',N'/calender',N'Action',N'RW',2,38,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(41,N'Wallet',NULL,N'Menu',NULL,1,NULL,4,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(42,N'Create Wallet',N'/createWallet',N'Menu',N'R',2,41,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(43,N'Create Wallet',N'/createWallet',N'Menu',N'RW',2,41,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(44,N'Wallet List',N'/walletList',N'Menu',N'R',2,41,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(45,N'Wallet List',N'/walletList',N'Menu',N'RW',2,41,2,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(46,N'Wallet Transfer',N'/walletTransfer',N'Menu',N'R',2,41,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(47,N'Wallet Transfer',N'/walletTransfer',N'Menu',N'RW',2,41,3,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(48,N'Wallet Transaction List',N'/walletTransactionList',N'Action',N'R',NULL,41,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
 ,(49,N'Wallet Transaction List',N'/walletTransactionList',N'Action',N'RW',NULL,41,1,'2025-02-21T19:36:29.653',N'Admin',NULL,NULL,0)
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
(2,14),
(2,16),
(2,18),
(2,19),
(2,21),
(2,23),
(2,24),
(2,26),
(2,28),
(1,24),
(1,26),
(1,28),
(2,29),
(2,31),
(2,33),
(2,38),
(2,40),
(2,41),
(2,43),
(2,45),
(2,47),
(2,49)