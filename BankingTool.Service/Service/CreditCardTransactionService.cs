using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class CreditCardTransactionService(
        ICreditCardTransactionRepository transactionRepository,
        ICreditCardRepository creditCardRepository,
        ICreditCardStatementRepository statementRepository,
        IBudgetRepository budgetRepository,
        IBankAccountRepository bankAccountRepository) : ICreditCardTransactionService
    {
        private readonly ICreditCardTransactionRepository _transactionRepository = transactionRepository;
        private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;
        private readonly ICreditCardStatementRepository _statementRepository = statementRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;


        public async Task<ResponseDto<bool>> RecordTransaction(int creditCardId, TransactionType transactionType,
            decimal amount, string description, string merchantName, List<RecordCreditCardTransactionRequestBudgetAccount> budgetAccounts, DateTime transactionDate)
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

                var creditCard = await _creditCardRepository.GetCreditCardById(creditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                // Validate card status
                if (creditCard.CardStatus != CreditCardStatus.Active)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card is not active" });
                    response.StatuCode = 400;
                    return response;
                }

                // For purchases, validate available credit
                if (transactionType == TransactionType.Purchase && amount > creditCard.CreditLimit)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Insufficient available credit" });
                    response.StatuCode = 400;
                    return response;
                }
                List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(x => new BudgetAccountDto
                {
                    Id = x.Id,
                    Amount = x.Amount
                })];
                var validationResult = ValidateBudgetAccount(validateBudgetAccount, amount);
                if (validationResult != null)
                {
                    return new ResponseDto<bool>
                    {
                        Status = false,
                        ValidationErrors = validationResult,
                        Errors = [],
                        StatuCode = 400
                    };
                }

                // referenceNumber need to be generate randomly 5 digit number
                var referenceNumber = GenerateReferenceNumber();

                var transaction = new CreditCardTransaction
                {
                    CreditCardId = creditCardId,
                    TransactionDate = transactionDate,
                    TransactionType = transactionType,
                    Amount = amount,
                    Description = description,
                    MerchantName = merchantName,
                    TransactionStatus = TransactionStatus.Completed,
                    ReferenceNumber = referenceNumber,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                UpdateCardBalance(creditCard, transactionType, amount);

                List<BudgetTransactionDto> budgetAccountList = [];
                if (budgetAccounts != null && budgetAccounts.Count > 0)
                {
                    foreach (var bAccount in budgetAccounts)
                    {
                        BudgetTransaction lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                        {
                            return new ResponseDto<bool>
                            {
                                Status = false,
                                ValidationErrors = [],
                                Errors = [new Errors { PropertyName = "CashWithdraw", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                            };
                        }
                        DateTime budgetTransactionDate = transactionDate;
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                        {
                            budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                            budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                        }
                        BudgetAccount budgetAccount = await _budgetRepository.GetBudgetAccountById(bAccount.Id);
                        budgetAccount.Balance = budgetAccount.Balance - bAccount.Amount;
                        BudgetTransaction budgetTransaction = new BudgetTransaction()
                        {
                            Amount = bAccount.Amount,
                            Description = description,
                            TransactionTime = budgetTransactionDate,
                            TransactionCategory = TransactionCategoryEnum.Transfer.EnumToString(),
                            TransactionTag = TransactionTagEnum.Transfer.EnumToString()
                        };
                        BudgetTransactionDetail budgetTransactionDetails = new BudgetTransactionDetail()
                        {
                            TransactionType = TransactionTypeEnum.Debit.EnumToString(),
                            StageBalance = budgetAccount.Balance,
                            TransactionRole = TransactionRoleEnum.Sender.EnumToString(),
                            CreditCardId = creditCardId,
                            BudgetAccountId = budgetAccount.BudgetAccountId
                        };

                        BudgetTransactionDto budgetTransactions = new BudgetTransactionDto()
                        {
                            BudgetAccount = budgetAccount,
                            BudgetTransaction = budgetTransaction,
                            BudgetTransactionDetail = budgetTransactionDetails
                        };
                        budgetAccountList.Add(budgetTransactions);
                    }
                }

                var saveResult = await _transactionRepository.SaveCreditCardTransaction(transaction, creditCard, budgetAccountList);
                if (saveResult.Success)
                {
                    response.Status = true;
                    response.Message = "Transaction recorded successfully";
                    response.Response = true;
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
        private static string GenerateReferenceNumber()
        {
            var random = new Random();
            return random.Next(10000, 99999).ToString(); // Generates a random 5-digit number
        }

        public async Task<ResponseDto<bool>> ProcessPayment(int creditCardId, decimal paymentAmount, string description, DateTime transactionDate, decimal fees)
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
                var creditCard = await _creditCardRepository.GetCreditCardById(creditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                if (paymentAmount > creditCard.LimitUsed)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Payment amount exceeds current balance" });
                    response.StatuCode = 400;
                    return response;
                }

                var transaction = new CreditCardTransaction
                {
                    CreditCardId = creditCardId,
                    TransactionDate = transactionDate,
                    TransactionType = TransactionType.Payment,
                    Amount = paymentAmount,
                    Description = description ?? "Payment",
                    TransactionStatus = TransactionStatus.Completed,
                    ReferenceNumber = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                // Update card balance
                creditCard.LimitUsed -= paymentAmount;
                creditCard.CreditLimit += paymentAmount;
                creditCard.ModifiedDate = DateTime.Now;

                var previousStatement = await _statementRepository.GetCurrentStatement(creditCardId);
                DateTime billingCycleStartDate = previousStatement?.BillingCycleEndDate != null ? previousStatement.BillingCycleEndDate.AddDays(1) : DateTime.Now.AddMonths(-1);

                var transactions = await _transactionRepository.GetTransactionsByDateRange(creditCardId, billingCycleStartDate, transactionDate);

                var statement = new CreditCardStatement
                {
                    CreditCardId = creditCardId,
                    BillingCycleStartDate = billingCycleStartDate,
                    BillingCycleEndDate = transactionDate,
                    StatementDate = transactionDate,
                    TotalPayments = paymentAmount,
                    Fees = fees,
                    StatementStatus = creditCard.LimitUsed == 0 ? StatementStatus.Paid : StatementStatus.PartiallyPaid,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                var paymentSaved = await _transactionRepository.ProcessPaymentWithStatement(transaction, creditCard, statement, transactions);
                if (!paymentSaved)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to process payment" });
                    response.StatuCode = 500;
                    return response;
                }

                response.Status = true;
                response.StatuCode = 201;

                response.Status = true;
                response.Message = "Payment processed and statement generated successfully";
                response.Response = true;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while processing payment: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<TransactionResponseListObject>> GetTransactionHistory(int creditCardId)
        {
            var response = new ResponseDto<TransactionResponseListObject>
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
                    response.Errors.Add(new Errors { ErrorMessage = "Provided creditCardId is invalid" });
                    response.StatuCode = 400;
                    return response;
                }

                var transactions = await _transactionRepository.GetTransactionsByCardId(creditCardId);

                response.Status = true;
                response.Message = $"Retrieved {transactions.Count} transactions";
                response.Response = new TransactionResponseListObject
                {
                    Response = [.. transactions.Select(transaction => new TransactionResponse
                    {
                        Transaction = new TransactionResponseTransaction
                        {
                            Id = transaction.TransactionId,
                            TransactionDate = transaction.TransactionDate,
                            TransactionType = transaction.TransactionType.EnumToString(),
                            Amount = transaction.Amount,
                            Description = transaction.Description,
                            MerchantName = transaction.MerchantName,
                            TransactionStatus = transaction.TransactionStatus.EnumToString(),
                            ReferenceNumber = transaction.ReferenceNumber
                        },
                        CreditCard = new TransactionResponseCreditCard
                        {
                            Id = transaction.CreditCardId,
                        },
                        Account = null
                    })]
                };
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving transaction history: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        private void UpdateCardBalance(CreditCard creditCard, TransactionType transactionType, decimal amount)
        {
            if (transactionType == TransactionType.Purchase)
            {
                creditCard.LimitUsed += amount;
                creditCard.CreditLimit -= amount;
            }
            else if (transactionType == TransactionType.Fee || transactionType == TransactionType.Interest)
            {
                creditCard.LimitUsed += amount;
                creditCard.CreditLimit -= amount;
            }

            creditCard.ModifiedDate = DateTime.Now;
        }
        private static List<ValidationResults> ValidateBudgetAccount(List<BudgetAccountDto> budgetAccounts, decimal amount)
        {
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                bool isInValid = budgetAccounts.Any(x => x.Id == null);
                bool isAmountInValid = budgetAccounts.Any(x => x?.Amount == null || x.Amount == 0);
                decimal sumOfBudgetAccountAmount = budgetAccounts.Sum(x => x.Amount);
                if (sumOfBudgetAccountAmount > amount)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Sum of Budget Account Amount should be lesser equal to Transfer Amount." }];
                }
                if (isInValid)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Id", PropertyName = "BudgetAccountId", ErrorMessage = "BudgetAccountId is required." }];
                }
                if (isAmountInValid)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Amount is required." }];
                }
            }
            return null;
        }
    }
}
