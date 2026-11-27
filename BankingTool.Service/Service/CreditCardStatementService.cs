using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class CreditCardStatementService(
        ICreditCardStatementRepository statementRepository) : ICreditCardStatementService
    {
        private readonly ICreditCardStatementRepository _statementRepository = statementRepository;

        public async Task<ResponseDto<StatementResponseObject>> GetStatementWithDetails(int statementId)
        {
            var response = new ResponseDto<StatementResponseObject>
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
                    response.StatuCode = 200;
                    return response;
                }

                var statementDetails = await _statementRepository.GetStatementDetails(statementId);
                var statementDTO = new StatementResponseObject()
                {
                    Response = new StatementResponse
                    {
                        Statement = new StatementResponseStatement
                        {
                            Id = statement.StatementId,
                            BillingCycleStartDate = statement.BillingCycleStartDate,
                            BillingCycleEndDate = statement.BillingCycleEndDate,
                            StatementDate = statement.StatementDate,
                            TotalPayments = statement.TotalPayments,
                            Fees = statement.Fees,
                            StatementStatus = statement.StatementStatus.EnumToString(),
                            StatementDetails = statementDetails.Select(detail => new StatementResponseStatementDetail
                            {
                                Id = detail.StatementDetailId,
                                TransactionDate = detail.TransactionDate,
                                Description = detail.Description,
                                Amount = detail.Amount,
                                TransactionType = detail.TransactionType
                            }).ToList()
                        }
                    }
                };

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

        public async Task<ResponseDto<StatementResponseListObject>> GetStatementsByCardId(int creditCardId)
        {
            var response = new ResponseDto<StatementResponseListObject>
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

                var statement = await _statementRepository.GetStatementsByCardId(creditCardId);

                response.Status = true;
                response.Message = "Statements retrieved successfully";
                response.Response = statement == null ? null : new StatementResponseListObject
                {
                    Responses = statement.Select(s => new StatementResponse
                    {
                        Statement = new StatementResponseStatement
                        {
                            Id = s.StatementId,
                            BillingCycleStartDate = s.BillingCycleStartDate,
                            BillingCycleEndDate = s.BillingCycleEndDate,
                            StatementDate = s.StatementDate,
                            TotalPayments = s.TotalPayments,
                            Fees = s.Fees,
                            StatementStatus = s.StatementStatus.EnumToString()
                        }
                    }).ToList()
                };
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving statements: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }
    }
}
