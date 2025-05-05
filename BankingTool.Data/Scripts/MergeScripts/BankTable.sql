SET IDENTITY_INSERT [Bank] ON

MERGE INTO [Bank] AS Target
USING (VALUES
  (1,N'State Bank of India',N'SBI')
 ,(2,N'HDFC Bank',N'HDFC')
 ,(3,N'ICICI Bank',N'ICICI')
 ,(4,N'Axis Bank',N'AXIS')
 ,(5,N'Punjab National Bank',N'PNB')
 ,(6,N'Bank of Baroda',N'BOB')
 ,(7,N'Canara Bank',N'CANARA')
 ,(8,N'Kotak Mahindra Bank',N'KOTAK')
 ,(9,N'Union Bank of India',N'UBI')
 ,(10,N'IDBI Bank',N'IDBI')
 ,(11,N'Indian Overseas Bank',N'IOB')
 ,(12,N'South Indian Bank',N'SIB')
) AS Source ([BankId],[BankName],[BankAbbrivation])
ON (Target.[BankId] = Source.[BankId])
WHEN MATCHED AND (
	NULLIF(Source.[BankName], Target.[BankName]) IS NOT NULL OR NULLIF(Target.[BankName], Source.[BankName]) IS NOT NULL OR 
	NULLIF(Source.[BankAbbrivation], Target.[BankAbbrivation]) IS NOT NULL OR NULLIF(Target.[BankAbbrivation], Source.[BankAbbrivation]) IS NOT NULL) THEN
 UPDATE SET
  [BankName] = Source.[BankName], 
  [BankAbbrivation] = Source.[BankAbbrivation]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([BankId],[BankName],[BankAbbrivation])
 VALUES(Source.[BankId],Source.[BankName],Source.[BankAbbrivation])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Bank]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Bank] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Bank] OFF
GO