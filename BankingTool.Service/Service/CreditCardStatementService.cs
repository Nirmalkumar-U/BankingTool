using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    /// <summary>
    /// Service for handling credit card statements including generation, calculation of minimum due, and interest
    /// </summary>
    public class CreditCardStatementService(
        ICreditCardStatementRepository statementRepository,
        ICreditCardTransactionRepository transactionRepository,
        ICreditCardRepository creditCardRepository) : ICreditCardStatementService
    {
        private readonly ICreditCardStatementRepository _statementRepository = statementRepository;
        private readonly ICreditCardTransactionRepository _transactionRepository = transactionRepository;
        private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;

        // Default monthly interest rate (2.5% per month = 30% per annum)
        private const decimal DefaultMonthlyInterestRate = 0.025m;

        public async Task<ResponseDto<StatementDTO>> GenerateStatement(int creditCardId, DateTime billingCycleEndDate)
        {
            var response = new ResponseDto<StatementDTO>
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

                // Get the previous statement to determine billing cycle start date
                var previousStatement = await _statementRepository.GetCurrentStatement(creditCardId);
                var billingCycleStartDate = previousStatement?.BillingCycleEndDate ?? DateTime.UtcNow.AddMonths(-1);

                // Calculate transactions for this billing cycle
                var transactions = await _transactionRepository.GetTransactionsByDateRange(creditCardId, billingCycleStartDate, billingCycleEndDate);

                decimal totalCharges = 0;
                decimal totalPayments = 0;

                foreach (var transaction in transactions)
                {
                    if (transaction.TransactionType == TransactionType.Payment || transaction.TransactionType == TransactionType.Refund)
                    {
                        totalPayments += transaction.Amount;
                    }
                    else
                    {
                        totalCharges += transaction.Amount;
                    }
                }

                var previousBalance = previousStatement?.CardBalance ?? 0;
                var interest = CalculateInterest(previousBalance, DefaultMonthlyInterestRate);
                var fees = 0m; // This would normally be calculated separately or fetched from fee service
                var newBalance = previousBalance + totalCharges + interest + fees - totalPayments;
                var minimumDue = CalculateMinimumDue(newBalance);
                var dueDate = billingCycleEndDate.AddDays(20); // 20 days to pay

                var statement = new CreditCardStatement
                {
                    CreditCardId = creditCardId,
                    BillingCycleStartDate = billingCycleStartDate,
                    BillingCycleEndDate = billingCycleEndDate,
                    StatementDate = DateTime.UtcNow,
                    TotalCharges = totalCharges,
                    TotalPayments = totalPayments,
                    Interest = interest,
                    Fees = fees,
                    PreviousBalance = previousBalance,
                    CardBalance = newBalance,
                    MinimumDueAmount = minimumDue,
                    DueDate = dueDate,
                    StatementStatus = StatementStatus.Generated,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                var statementId = await _statementRepository.InsertStatement(statement);

                if (statementId > 0)
                {
                    response.Status = true;
                    response.Message = "Statement generated successfully";
                    response.Response = MapToStatementDTO(statement);
                    response.StatuCode = 201;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to generate statement" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while generating statement: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public decimal CalculateMinimumDue(decimal balance)
        {
            // Minimum due is typically 5% of balance or minimum threshold (e.g., $25 or $50)
            const decimal minimumDuePercentage = 0.05m;
            const decimal minimumThreshold = 25m;

            var calculatedMinimumDue = balance * minimumDuePercentage;
            return calculatedMinimumDue < minimumThreshold ? minimumThreshold : calculatedMinimumDue;
        }

        public decimal CalculateInterest(decimal balance, decimal monthlyInterestRate)
        {
            // Only charge interest if there's a balance
            return balance > 0 ? balance * monthlyInterestRate : 0;
        }

        public async Task<ResponseDto<StatementDTO>> GetStatementDetails(int statementId)
        {
            var response = new ResponseDto<StatementDTO>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (statementId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid statement ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var statement = await _statementRepository.GetStatementById(statementId);
                if (statement == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Statement not found" });
                    response.StatuCode = 404;
                    return response;
                }

                response.Status = true;
                response.Message = "Statement details retrieved successfully";
                response.Response = MapToStatementDTO(statement);
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving statement details: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<StatementDTO>> GetStatementWithDetails(int statementId)
        {
            var response = new ResponseDto<StatementDTO>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (statementId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid statement ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var statement = await _statementRepository.GetStatementById(statementId);
                if (statement == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Statement not found" });
                    response.StatuCode = 404;
                    return response;
                }

                var statementDetails = await _statementRepository.GetStatementDetails(statementId);
                var statementDTO = MapToStatementDTO(statement);

                response.Status = true;
                response.Message = "Statement retrieved successfully";
                response.Response = statementDTO;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving statement: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<StatementDTO>>> GetStatementHistory(int creditCardId, int pageNumber = 1, int pageSize = 10)
        {
            var response = new ResponseDto<List<StatementDTO>>
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

                var statements = await _statementRepository.GetStatementsByCardIdWithPagination(creditCardId, pageNumber, pageSize);

                response.Status = true;
                response.Message = $"Retrieved {statements.Count} statements";
                response.Response = statements.Select(MapToStatementDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving statement history: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<StatementDTO>> GetCurrentStatement(int creditCardId)
        {
            var response = new ResponseDto<StatementDTO>
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

                var statement = await _statementRepository.GetCurrentStatement(creditCardId);
                if (statement == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "No statement found for this card" });
                    response.StatuCode = 404;
                    return response;
                }

                response.Status = true;
                response.Message = "Current statement retrieved successfully";
                response.Response = MapToStatementDTO(statement);
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving current statement: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<StatementDTO>>> GetStatementsByStatus(int creditCardId, StatementStatus status)
        {
            var response = new ResponseDto<List<StatementDTO>>
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

                var statements = await _statementRepository.GetStatementsByStatus(creditCardId, status);

                response.Status = true;
                response.Message = $"Retrieved {statements.Count} statements";
                response.Response = statements.Select(MapToStatementDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving statements: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<StatementDTO>>> GetOverdueStatements(int creditCardId)
        {
            var response = new ResponseDto<List<StatementDTO>>
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

                var statements = await _statementRepository.GetOverdueStatements(creditCardId);

                response.Status = true;
                response.Message = $"Retrieved {statements.Count} overdue statements";
                response.Response = statements.Select(MapToStatementDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving overdue statements: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> MarkStatementAsPaid(int statementId)
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
                if (statementId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid statement ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var statement = await _statementRepository.GetStatementById(statementId);
                if (statement == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Statement not found" });
                    response.StatuCode = 404;
                    return response;
                }

                statement.StatementStatus = StatementStatus.Paid;
                statement.ModifiedDate = DateTime.UtcNow;

                var updated = await _statementRepository.UpdateStatement(statement);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Statement marked as paid successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to mark statement as paid" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while marking statement as paid: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> MarkStatementAsPartiallyPaid(int statementId)
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
                if (statementId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid statement ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var statement = await _statementRepository.GetStatementById(statementId);
                if (statement == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Statement not found" });
                    response.StatuCode = 404;
                    return response;
                }

                statement.StatementStatus = StatementStatus.PartiallyPaid;
                statement.ModifiedDate = DateTime.UtcNow;

                var updated = await _statementRepository.UpdateStatement(statement);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Statement marked as partially paid successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to mark statement as partially paid" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while marking statement as partially paid: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> MarkStatementAsOverdue(int statementId)
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
                if (statementId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid statement ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var statement = await _statementRepository.GetStatementById(statementId);
                if (statement == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Statement not found" });
                    response.StatuCode = 404;
                    return response;
                }

                statement.StatementStatus = StatementStatus.Overdue;
                statement.ModifiedDate = DateTime.UtcNow;

                var updated = await _statementRepository.UpdateStatement(statement);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Statement marked as overdue successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to mark statement as overdue" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while marking statement as overdue: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        private StatementDTO MapToStatementDTO(CreditCardStatement statement)
        {
            return new StatementDTO
            {
                StatementId = statement.StatementId,
                BillingCycleStartDate = statement.BillingCycleStartDate,
                BillingCycleEndDate = statement.BillingCycleEndDate,
                TotalCharges = statement.TotalCharges,
                TotalPayments = statement.TotalPayments,
                CardBalance = statement.CardBalance,
                MinimumDueAmount = statement.MinimumDueAmount,
                DueDate = statement.DueDate,
                StatementStatus = statement.StatementStatus.ToString()
            };
        }
    }
}
