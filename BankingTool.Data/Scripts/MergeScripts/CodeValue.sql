SET IDENTITY_INSERT [CodeValue] ON

MERGE INTO [CodeValue] AS Target
USING (VALUES
  (1,N'AccountType',100,N'Saving Account',N'SA',NULL,N'Bank account type is savings account',NULL,NULL,1,1)
 ,(2,N'AccountType',100,N'Current Account',N'CA',NULL,N'Bank account type is current account',NULL,NULL,1,2)
 ,(3,N'AccountType',100,N'Fixed Deposit Account',N'FD',NULL,N'Bank account type is Fixed Deposit Account',NULL,NULL,1,3)
 ,(4,N'AccountType',100,N'Recurring Deposit Account',N'RD',NULL,N'Bank account type is Recurring Deposit Account',NULL,NULL,1,4)
 ,(5,N'TransactionTag',101,N'Entertainment',NULL,NULL,N'Transaction happened for Entertainment',NULL,NULL,1,1)
 ,(6,N'TransactionTag',101,N'No Tag',NULL,NULL,N'No Tag For this Transaction',NULL,NULL,1,2)
 ,(7,N'TransactionTag',101,N'Transfer',NULL,NULL,N'Transaction happened for Transfer',NULL,NULL,1,3)
 ,(8,N'TransactionTag',101,N'Groceries',NULL,NULL,N'Transaction happened for Groceries',NULL,NULL,1,4)
 ,(9,N'TransactionTag',101,N'Miscellaneous',NULL,NULL,N'Transaction happened for Miscellaneous',NULL,NULL,1,5)
 ,(10,N'TransactionTag',101,N'Self Transfer',NULL,NULL,N'Transaction happened for Self Transfer',NULL,NULL,1,6)
 ,(11,N'TransactionTag',101,N'Food',NULL,NULL,N'Transaction happened for Food',NULL,NULL,1,7)
 ,(12,N'TransactionTag',101,N'Fuel',NULL,NULL,N'Transaction happened for Fuel',NULL,NULL,1,8)
 ,(13,N'TransactionTag',101,N'Bill Payment',NULL,NULL,N'Transaction happened for Bill Payment',NULL,NULL,1,9)
 ,(14,N'TransactionTag',101,N'Shopping',NULL,NULL,N'Transaction happened for Shopping',NULL,NULL,1,10)
 ,(15,N'TransactionTag',101,N'Rent',NULL,NULL,N'Transaction happened for Rent',NULL,NULL,1,11)
 ,(16,N'TransactionTag',101,N'Travel',NULL,NULL,N'Transaction happened for Travel',NULL,NULL,1,12)
 ,(17,N'TransactionTag',101,N'Health',NULL,NULL,N'Transaction happened for Health',NULL,NULL,1,13)
 ,(18,N'TransactionTag',101,N'Insurance',NULL,NULL,N'Transaction happened for Insurance',NULL,NULL,1,14)
 ,(19,N'TransactionTag',101,N'Investment',NULL,NULL,N'Transaction happened for Investment',NULL,NULL,1,15)
 ,(20,N'TransactionTag',101,N'Salary',NULL,NULL,N'Transaction happened for Salary',NULL,NULL,1,16)
 ,(21,N'TransactionTag',101,N'Savings',NULL,NULL,N'Transaction happened for Savings',NULL,NULL,1,17)
 ,(22,N'TransactionCategory',102,N'Cash',NULL,NULL,N'Transaction happened for cash',NULL,NULL,0,1)
 ,(23,N'TransactionCategory',102,N'Transfer',NULL,NULL,N'Transaction happened for Transfer',NULL,NULL,1,2)
 ,(24,N'TransactionCategory',102,N'Cheque',NULL,NULL,N'Transaction happened for Cheque',NULL,NULL,0,3)
 ,(25,N'TransactionCategory',102,N'Deposit',NULL,NULL,N'Transaction happened for Deposit',NULL,NULL,1,4)
 ,(26,N'TransactionCategory',102,N'Withdraw',NULL,NULL,N'Transaction happened for Withdraw',NULL,NULL,1,5)
) AS Source ([CodeValueId],[TypeName],[TypeCode],[CodeValue1],[CodeValue2],[CodeValue3],[CodeValue1Description],[CodeValue2Description],[CodeValue3Description],[InUse],[Sequence])
ON (Target.[CodeValueId] = Source.[CodeValueId])
WHEN MATCHED AND (
	NULLIF(Source.[TypeName], Target.[TypeName]) IS NOT NULL OR NULLIF(Target.[TypeName], Source.[TypeName]) IS NOT NULL OR 
	NULLIF(Source.[TypeCode], Target.[TypeCode]) IS NOT NULL OR NULLIF(Target.[TypeCode], Source.[TypeCode]) IS NOT NULL OR 
	NULLIF(Source.[CodeValue1], Target.[CodeValue1]) IS NOT NULL OR NULLIF(Target.[CodeValue1], Source.[CodeValue1]) IS NOT NULL OR 
	NULLIF(Source.[CodeValue2], Target.[CodeValue2]) IS NOT NULL OR NULLIF(Target.[CodeValue2], Source.[CodeValue2]) IS NOT NULL OR 
	NULLIF(Source.[CodeValue3], Target.[CodeValue3]) IS NOT NULL OR NULLIF(Target.[CodeValue3], Source.[CodeValue3]) IS NOT NULL OR 
	NULLIF(Source.[CodeValue1Description], Target.[CodeValue1Description]) IS NOT NULL OR NULLIF(Target.[CodeValue1Description], Source.[CodeValue1Description]) IS NOT NULL OR 
	NULLIF(Source.[CodeValue2Description], Target.[CodeValue2Description]) IS NOT NULL OR NULLIF(Target.[CodeValue2Description], Source.[CodeValue2Description]) IS NOT NULL OR 
	NULLIF(Source.[CodeValue3Description], Target.[CodeValue3Description]) IS NOT NULL OR NULLIF(Target.[CodeValue3Description], Source.[CodeValue3Description]) IS NOT NULL OR 
	NULLIF(Source.[InUse], Target.[InUse]) IS NOT NULL OR NULLIF(Target.[InUse], Source.[InUse]) IS NOT NULL OR 
	NULLIF(Source.[Sequence], Target.[Sequence]) IS NOT NULL OR NULLIF(Target.[Sequence], Source.[Sequence]) IS NOT NULL) THEN
 UPDATE SET
  [TypeName] = Source.[TypeName], 
  [TypeCode] = Source.[TypeCode], 
  [CodeValue1] = Source.[CodeValue1], 
  [CodeValue2] = Source.[CodeValue2], 
  [CodeValue3] = Source.[CodeValue3], 
  [CodeValue1Description] = Source.[CodeValue1Description], 
  [CodeValue2Description] = Source.[CodeValue2Description], 
  [CodeValue3Description] = Source.[CodeValue3Description], 
  [InUse] = Source.[InUse], 
  [Sequence] = Source.[Sequence]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([CodeValueId],[TypeName],[TypeCode],[CodeValue1],[CodeValue2],[CodeValue3],[CodeValue1Description],[CodeValue2Description],[CodeValue3Description],[InUse],[Sequence])
 VALUES(Source.[CodeValueId],Source.[TypeName],Source.[TypeCode],Source.[CodeValue1],Source.[CodeValue2],Source.[CodeValue3],Source.[CodeValue1Description],Source.[CodeValue2Description],Source.[CodeValue3Description],Source.[InUse],Source.[Sequence])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [CodeValue]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[CodeValue] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [CodeValue] OFF
GO