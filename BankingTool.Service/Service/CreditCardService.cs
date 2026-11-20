using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    /// <summary>
    /// Service for handling credit card operations including applications, approvals, and card management
    /// </summary>
    public class CreditCardService(
        ICreditCardRepository creditCardRepository) : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;

        public async Task<ResponseDto<CreditCardApplicationResponseDTO>> ApplyCreditCard(CreditCardApplicationRequestDTO request)
        {
            var response = new ResponseDto<CreditCardApplicationResponseDTO>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            try
            {
                if (request == null || request.AccountId <= 0 || string.IsNullOrEmpty(request.CardType) || request.DesiredLimit <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card application request" });
                    response.StatuCode = 400;
                    return response;
                }
                // create a new credit card entity
                var creditCard = new CreditCard
                {
                    AccountId = request.AccountId,
                    CardType = request.CardType,
                    CreditLimit = request.DesiredLimit,
                    CardStatus = CreditCardStatus.Active,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ApplicationDate = DateTime.Now,
                    CardNumber = GenerateCardNumber(),
                    ExpiryDate = DateTime.Now.AddYears(5),
                    ApprovedDate = DateTime.Now,
                    CVV = GenerateCVV(),
                    ApprovedLimit = request.DesiredLimit,
                    AvailableCredit = request.DesiredLimit,
                    DecisionRemarks = "Approved by system",
                    IsDeleted = false,
                    LimitUsed = 0,
                    IssueDate = DateTime.Now,
                    RequestedLimit = request.DesiredLimit,
                };

                var creditCardId = await _creditCardRepository.InsertCreditCard(creditCard);

                if (creditCardId > 0)
                {
                    response.Status = true;
                    response.Message = "Credit card application submitted successfully";
                    response.Response = new CreditCardApplicationResponseDTO
                    {
                        ApplicationId = creditCardId,
                        Status = creditCard.CardStatus.ToString(),
                        ApplicationDate = creditCard.ApplicationDate,
                        ApprovedDate = creditCard.ApprovedDate,
                        ApprovedLimit = creditCard.ApprovedLimit
                    };
                    response.StatuCode = 201;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to create credit card application" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while applying for credit card: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> UpdateCardLimit(int creditCardId, decimal newLimit)
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
                if (creditCardId <= 0 || newLimit <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID or new limit" });
                    response.StatuCode = 400;
                    return response;
                }

                var updated = await _creditCardRepository.UpdateCreditLimit(creditCardId, newLimit);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Credit limit updated successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to update credit limit due to limit constraints" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while updating credit limit: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> CloseCreditCard(int creditCardId, string closureReason)
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

                if (creditCard.LimitUsed > 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Cannot close card with outstanding balance" });
                    response.StatuCode = 400;
                    return response;
                }

                var updated = await _creditCardRepository.CloseCreditCard(creditCardId, closureReason);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Credit card closed successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to close credit card" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while closing credit card: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> BlockCreditCard(int creditCardId, string blockReason)
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
                if (creditCardId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var updated = await _creditCardRepository.BlockCreditCard(creditCardId, blockReason);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Credit card blocked successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to block credit card" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while blocking credit card: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> UnblockCreditCard(int creditCardId)
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
                if (creditCardId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var updated = await _creditCardRepository.UnblockCreditCard(creditCardId);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Credit card unblocked successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to unblock credit card" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while unblocking credit card: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<CreditCardDTO>> GetCardDetails(int creditCardId)
        {
            var response = new ResponseDto<CreditCardDTO>
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
                response.Message = "Credit card details retrieved successfully";
                response.Response = MapToCreditCardDTO(creditCard);
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving credit card details: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<CreditCardDTO>>> GetCustomerCreditCards()
        {
            var response = new ResponseDto<List<CreditCardDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                var creditCards = await _creditCardRepository.GetActiveCreditCardsByCustomerId();
                response.Status = true;
                response.Message = $"Retrieved {creditCards.Count} active credit cards";
                response.Response = creditCards.Select(MapToCreditCardDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving customer credit cards: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        private CreditCardDTO MapToCreditCardDTO(CreditCard creditCard)
        {
            var cardNumber = new CreditCardNumber(creditCard.CardNumber);
            return new CreditCardDTO
            {
                CreditCardId = creditCard.CreditCardId,
                AccountId = creditCard.AccountId,
                AccountNumber = creditCard.Account?.AccountNumber,
                AccountAbbreviation = creditCard.Account?.Bank?.BankAbbrivation,
                AccountDetail = creditCard.Account?.Bank != null
                    ? $"{creditCard.Account.Bank.BankAbbrivation} | {creditCard.CardNumber[^4..]}"
                    : creditCard.CardNumber[^4..],
                CardNumber = cardNumber,
                CardHolderName = creditCard.CardHolderName,
                CardType = creditCard.CardType,
                Status = creditCard.CardStatus.EnumToString(),
                CreditLimit = creditCard.CreditLimit ?? 0,
                BalanceCredit = creditCard.CreditLimit ?? 0 - creditCard.LimitUsed,
                AvailableCredit = creditCard.AvailableCredit ?? 0,
                ApplicationDate = creditCard.ApplicationDate,
                ApprovedDate = creditCard.ApprovedDate,
                ApprovedLimit = creditCard.ApprovedLimit,
                CurrentLimit = creditCard.CreditLimit,
                ExpiryDate = creditCard.ExpiryDate,
                LimitUsed = creditCard.LimitUsed
            };
        }


        private string GenerateCardNumber()
        {
            // Generate a simple 16-digit card number
            var random = new Random();
            var cardNumber = "";
            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(0, 10);
            }
            return cardNumber;
        }

        private string GenerateCVV()
        {
            // Generate a 3-digit CVV
            var random = new Random();
            return random.Next(100, 999).ToString();
        }
    }
}
