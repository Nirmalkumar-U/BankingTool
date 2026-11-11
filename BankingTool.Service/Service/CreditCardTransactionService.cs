using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    /// <summary>
    /// Service for handling credit card transactions (purchases, payments, reversals)
    /// </summary>
    public class CreditCardTransactionService(
        ICreditCardTransactionRepository transactionRepository,
        ICreditCardRepository creditCardRepository) : ICreditCardTransactionService
    {
        private readonly ICreditCardTransactionRepository _transactionRepository = transactionRepository;
        private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;

        public async Task<ResponseDto<TransactionDTO>> RecordTransaction(int creditCardId, TransactionType transactionType, 
            decimal amount, string description, string merchantName, string referenceNumber)
        {
            var response = new ResponseDto<TransactionDTO>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (creditCardId <= 0 || amount <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID or amount" });
                    response.StatuCode = 400;
                    return response;
                }

                var creditCard = await _creditCardRepository.GetCreditCardById(creditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                // Validate card status
                if (creditCard.Status != CreditCardStatus.Active)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card is not active" });
                    response.StatuCode = 400;
                    return response;
                }

                // For purchases, validate available credit
                if (transactionType == TransactionType.Purchase && amount > creditCard.AvailableCredit)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Insufficient available credit" });
                    response.StatuCode = 400;
                    return response;
                }

                var transaction = new CreditCardTransaction
                {
                    CreditCardId = creditCardId,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = transactionType,
                    Amount = amount,
                    Description = description,
                    MerchantName = merchantName,
                    TransactionStatus = TransactionStatus.Completed,
                    ReferenceNumber = referenceNumber,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                var transactionId = await _transactionRepository.InsertTransaction(transaction);

                if (transactionId > 0)
                {
                    // Update card balance and available credit
                    await UpdateCardBalance(creditCard, transactionType, amount);

                    response.Status = true;
                    response.Message = "Transaction recorded successfully";
                    response.Response = MapToTransactionDTO(transaction);
                    response.StatuCode = 201;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to record transaction" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while recording transaction: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<TransactionDTO>> ProcessPayment(int creditCardId, decimal paymentAmount, string description)
        {
            var response = new ResponseDto<TransactionDTO>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (creditCardId <= 0 || paymentAmount <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID or payment amount" });
                    response.StatuCode = 400;
                    return response;
                }

                var creditCard = await _creditCardRepository.GetCreditCardById(creditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                if (paymentAmount > creditCard.CurrentBalance)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Payment amount exceeds current balance" });
                    response.StatuCode = 400;
                    return response;
                }

                var transaction = new CreditCardTransaction
                {
                    CreditCardId = creditCardId,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = TransactionType.Payment,
                    Amount = paymentAmount,
                    Description = description ?? "Payment",
                    TransactionStatus = TransactionStatus.Completed,
                    ReferenceNumber = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                var transactionId = await _transactionRepository.InsertTransaction(transaction);

                if (transactionId > 0)
                {
                    // Update card balance
                    creditCard.CurrentBalance -= paymentAmount;
                    creditCard.AvailableCredit += paymentAmount;
                    creditCard.ModifiedDate = DateTime.UtcNow;

                    await _creditCardRepository.UpdateCreditCard(creditCard);

                    response.Status = true;
                    response.Message = "Payment processed successfully";
                    response.Response = MapToTransactionDTO(transaction);
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to process payment" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while processing payment: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> ReverseTransaction(int transactionId, string reason)
        {
            var response = new ResponseDto<bool>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (transactionId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid transaction ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var transaction = await _transactionRepository.GetTransactionById(transactionId);
                if (transaction == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Transaction not found" });
                    response.StatuCode = 404;
                    return response;
                }

                if (transaction.TransactionStatus == TransactionStatus.Reversed)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Transaction has already been reversed" });
                    response.StatuCode = 400;
                    return response;
                }

                var creditCard = await _creditCardRepository.GetCreditCardById(transaction.CreditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Associated credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                // Reverse the transaction
                var reversed = await _transactionRepository.ReverseTransaction(transactionId, reason);

                if (reversed)
                {
                    // For purchases, refund the amount back to credit
                    if (transaction.TransactionType == TransactionType.Purchase)
                    {
                        creditCard.CurrentBalance -= transaction.Amount;
                        creditCard.AvailableCredit += transaction.Amount;
                    }
                    // For payments, reverse the credit
                    else if (transaction.TransactionType == TransactionType.Payment)
                    {
                        creditCard.CurrentBalance += transaction.Amount;
                        creditCard.AvailableCredit -= transaction.Amount;
                    }

                    creditCard.ModifiedDate = DateTime.UtcNow;
                    await _creditCardRepository.UpdateCreditCard(creditCard);

                    response.Status = true;
                    response.Message = "Transaction reversed successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to reverse transaction" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while reversing transaction: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<TransactionDTO>>> GetTransactionHistory(int creditCardId, int pageNumber = 1, int pageSize = 10)
        {
            var response = new ResponseDto<List<TransactionDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (creditCardId <= 0 || pageNumber < 1 || pageSize < 1)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid parameters" });
                    response.StatuCode = 400;
                    return response;
                }

                var transactions = await _transactionRepository.GetTransactionsByCardIdWithPagination(creditCardId, pageNumber, pageSize);

                response.Status = true;
                response.Message = $"Retrieved {transactions.Count} transactions";
                response.Response = transactions.Select(MapToTransactionDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving transaction history: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<TransactionDTO>>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate)
        {
            var response = new ResponseDto<List<TransactionDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (creditCardId <= 0 || fromDate > toDate)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid parameters" });
                    response.StatuCode = 400;
                    return response;
                }

                var transactions = await _transactionRepository.GetTransactionsByDateRange(creditCardId, fromDate, toDate);

                response.Status = true;
                response.Message = $"Retrieved {transactions.Count} transactions";
                response.Response = transactions.Select(MapToTransactionDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving transactions: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<TransactionDTO>>> GetTransactionsByType(int creditCardId, TransactionType transactionType)
        {
            var response = new ResponseDto<List<TransactionDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (creditCardId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var transactions = await _transactionRepository.GetTransactionsByType(creditCardId, transactionType);

                response.Status = true;
                response.Message = $"Retrieved {transactions.Count} transactions";
                response.Response = transactions.Select(MapToTransactionDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving transactions: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<decimal>> CalculateAvailableCredit(int creditCardId)
        {
            var response = new ResponseDto<decimal>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (creditCardId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var creditCard = await _creditCardRepository.GetCreditCardById(creditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                response.Status = true;
                response.Message = "Available credit calculated successfully";
                response.Response = creditCard.AvailableCredit;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while calculating available credit: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<TransactionDTO>>> GetRecentTransactions(int creditCardId, int count = 10)
        {
            var response = new ResponseDto<List<TransactionDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (creditCardId <= 0 || count < 1)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid parameters" });
                    response.StatuCode = 400;
                    return response;
                }

                var transactions = await _transactionRepository.GetRecentTransactions(creditCardId, count);

                response.Status = true;
                response.Message = $"Retrieved {transactions.Count} recent transactions";
                response.Response = transactions.Select(MapToTransactionDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving recent transactions: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<TransactionDTO>> GetTransactionDetails(int transactionId)
        {
            var response = new ResponseDto<TransactionDTO>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (transactionId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid transaction ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var transaction = await _transactionRepository.GetTransactionById(transactionId);
                if (transaction == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Transaction not found" });
                    response.StatuCode = 404;
                    return response;
                }

                response.Status = true;
                response.Message = "Transaction details retrieved successfully";
                response.Response = MapToTransactionDTO(transaction);
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving transaction details: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        private async Task UpdateCardBalance(CreditCard creditCard, TransactionType transactionType, decimal amount)
        {
            if (transactionType == TransactionType.Purchase)
            {
                creditCard.CurrentBalance += amount;
                creditCard.AvailableCredit -= amount;
            }
            else if (transactionType == TransactionType.Fee || transactionType == TransactionType.Interest)
            {
                creditCard.CurrentBalance += amount;
                creditCard.AvailableCredit -= amount;
            }

            creditCard.ModifiedDate = DateTime.UtcNow;
            await _creditCardRepository.UpdateCreditCard(creditCard);
        }

        private TransactionDTO MapToTransactionDTO(CreditCardTransaction transaction)
        {
            return new TransactionDTO
            {
                TransactionId = transaction.TransactionId,
                TransactionDate = transaction.TransactionDate,
                TransactionType = transaction.TransactionType.ToString(),
                Amount = transaction.Amount,
                Description = transaction.Description,
                MerchantName = transaction.MerchantName,
                TransactionStatus = transaction.TransactionStatus.ToString(),
                ReferenceNumber = transaction.ReferenceNumber
            };
        }
    }
}
