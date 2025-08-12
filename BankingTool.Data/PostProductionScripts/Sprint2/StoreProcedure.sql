IF OBJECT_ID('GetWalletAccountTransactions', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE GetWalletAccountTransactions;
END
GO

CREATE PROCEDURE GetWalletAccountTransactions
    @WalletId INT
AS
BEGIN

SELECT
	bt.TransactionTag AS TransactionTag,
	bt.TransactionTime AS TransactionDate,
	bt.Description AS Description,
	CASE WHEN btd.TransactionRole = 'Receiver' THEN bt.Amount ELSE NULL END AS ReceiverAmount,
	CASE WHEN btd.TransactionRole = 'Sender' THEN bt.Amount ELSE NULL END AS SenderAmount,
	btd.StageBalance AS StageBalance
FROM WalletTransaction bt
JOIN WalletTransactionDetail btd on bt.WalletTransactionId = btd.WalletTransactionDetailId
JOIN WalletAccount ba on btd.WalletAccountId = ba.WalletAccountId
WHERE ba.WalletAccountId = @WalletId
ORDER BY bt.TransactionTime DESC

END
GO
-----------------------------------------------------------------------------------------------------