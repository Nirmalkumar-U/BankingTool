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
                statementDTO.StatementDetails = statementDetails.Select(detail => new StatementDetailDTO
                {
                    StatementDetailId = detail.StatementDetailId,
                    TransactionDate = detail.TransactionDate,
                    Description = detail.Description,
                    Amount = detail.Amount,
                    TransactionType = detail.TransactionType
                }).ToList();

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

                response.Status = true;
                response.Message = "Current statement retrieved successfully";
                response.Response = statement == null ? null : MapToStatementDTO(statement);
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving current statement: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<StatementDTO>>> GetStatementsByCardId(int creditCardId)
        {
            var response = new ResponseDto<List<StatementDTO>>
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
                response.Response = statement == null ? [] : [.. statement.Select(x => MapToStatementDTO(x))];
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving statements: {ex.Message}" });
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
                StatementDate = statement.StatementDate,
                TotalPayments = statement.TotalPayments,
                Fees = statement.Fees,
                StatementStatus = statement.StatementStatus.EnumToString()
            };
        }
    }
}
