using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    /// <summary>
    /// Service for handling credit card operations including applications, approvals, and card management
    /// </summary>
    public class CreditCardService(
        ICreditCardRepository creditCardRepository,
        ICreditCardApplicationRepository creditCardApplicationRepository,
        ICreditCardCryptographyService cryptographyService) : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;
        private readonly ICreditCardApplicationRepository _creditCardApplicationRepository = creditCardApplicationRepository;
        private readonly ICreditCardCryptographyService _cryptographyService = cryptographyService;

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

                var application = new CreditCardApplication
                {
                    AccountId = request.AccountId,
                    CardType = request.CardType,
                    DesiredLimit = request.DesiredLimit,
                    Status = CreditCardApplicationStatus.Pending,
                    ApplicationDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                int applicationId = 0;
                try
                {

                    applicationId = await _creditCardApplicationRepository.InsertApplication(application);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (applicationId > 0)
                {
                    response.Status = true;
                    response.Message = "Credit card application submitted successfully";
                    response.Response = new CreditCardApplicationResponseDTO
                    {
                        ApplicationId = applicationId,
                        Status = application.Status.ToString(),
                        ApplicationDate = application.ApplicationDate,
                        ApprovedDate = application.ApprovedDate,
                        ApprovedLimit = application.ApprovedLimit
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

        public async Task<ResponseDto<bool>> ApproveCreditCard(int applicationId, decimal approvedLimit, string approvalRemarks)
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
                if (applicationId <= 0 || approvedLimit <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid application ID or approved limit" });
                    response.StatuCode = 400;
                    return response;
                }

                var application = await _creditCardApplicationRepository.GetApplicationById(applicationId);
                if (application == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card application not found" });
                    response.StatuCode = 404;
                    return response;
                }

                if (application.Status != CreditCardApplicationStatus.Pending && application.Status != CreditCardApplicationStatus.UnderReview)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Application cannot be approved in its current status" });
                    response.StatuCode = 400;
                    return response;
                }

                // Update application
                application.Status = CreditCardApplicationStatus.Approved;
                application.ApprovedLimit = approvedLimit;
                application.ApprovedDate = DateTime.UtcNow;
                application.DecisionRemarks = approvalRemarks;
                application.ModifiedDate = DateTime.UtcNow;

                var updated = await _creditCardApplicationRepository.UpdateApplication(application);

                if (updated)
                {
                    // Create actual credit card
                    var creditCard = CreateCreditCardFromApplication(application);
                    await _creditCardRepository.InsertCreditCard(creditCard);

                    response.Status = true;
                    response.Message = "Credit card application approved successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to approve credit card application" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while approving credit card: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> RejectCreditCard(int applicationId, string rejectionRemarks)
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
                if (applicationId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid application ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var application = await _creditCardApplicationRepository.GetApplicationById(applicationId);
                if (application == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card application not found" });
                    response.StatuCode = 404;
                    return response;
                }

                if (application.Status != CreditCardApplicationStatus.Pending && application.Status != CreditCardApplicationStatus.UnderReview)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Application cannot be rejected in its current status" });
                    response.StatuCode = 400;
                    return response;
                }

                application.Status = CreditCardApplicationStatus.Rejected;
                application.DecisionRemarks = rejectionRemarks;
                application.ModifiedDate = DateTime.UtcNow;

                var updated = await _creditCardApplicationRepository.UpdateApplication(application);

                if (updated)
                {
                    response.Status = true;
                    response.Message = "Credit card application rejected successfully";
                    response.Response = true;
                    response.StatuCode = 200;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to reject credit card application" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while rejecting credit card: {ex.Message}" });
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
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to update credit limit" });
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

                if (creditCard.CurrentBalance > 0)
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

        public async Task<ResponseDto<CreditCardApplicationResponseDTO>> GetApplicationStatus(int applicationId)
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
                if (applicationId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid application ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var application = await _creditCardApplicationRepository.GetApplicationById(applicationId);
                if (application == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Application not found" });
                    response.StatuCode = 404;
                    return response;
                }

                response.Status = true;
                response.Message = "Application status retrieved successfully";
                response.Response = MapToCreditCardApplicationResponseDTO(application);
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving application status: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<CreditCardApplicationResponseDTO>>> GetCustomerApplications(int customerId)
        {
            var response = new ResponseDto<List<CreditCardApplicationResponseDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (customerId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid customer ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var applications = await _creditCardApplicationRepository.GetApplicationsByCustomerId(customerId);

                response.Status = true;
                response.Message = $"Retrieved {applications.Count} applications";
                response.Response = applications.Select(MapToCreditCardApplicationResponseDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving customer applications: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<CreditCardApplicationResponseDTO>>> GetPendingApplications()
        {
            var response = new ResponseDto<List<CreditCardApplicationResponseDTO>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                var applications = await _creditCardApplicationRepository.GetApplicationsByStatus(CreditCardApplicationStatus.Pending);

                response.Status = true;
                response.Message = $"Retrieved {applications.Count} pending applications";
                response.Response = applications.Select(MapToCreditCardApplicationResponseDTO).ToList();
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving pending applications: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        private CreditCard CreateCreditCardFromApplication(CreditCardApplication application)
        {
            // Generate a fake card number (in production, use actual card generation service)
            var cardNumber = GenerateCardNumber();
            var cvv = GenerateCVV();
            var expiryDate = DateTime.UtcNow.AddYears(5);

            return new CreditCard
            {
                AccountId = application.AccountId,
                CardNumber = _cryptographyService.EncryptCardNumber(cardNumber),
                CVV = _cryptographyService.EncryptCVV(cvv),
                CardHolderName = "CARDHOLDER", // This should come from customer profile
                CardType = application.CardType,
                Status = CreditCardStatus.Active,
                IssueDate = DateTime.UtcNow,
                ExpiryDate = expiryDate,
                CreditLimit = application.ApprovedLimit ?? 0,
                CurrentBalance = 0,
                AvailableCredit = application.ApprovedLimit ?? 0,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };
        }

        private CreditCardDTO MapToCreditCardDTO(CreditCard creditCard)
        {
            string maskedCardNumber = "****";
            string maskedExpiryDate = "**/**";

            try
            {
                var decryptedCardNumber = _cryptographyService.DecryptCardNumber(creditCard.CardNumber);
                maskedCardNumber = _cryptographyService.MaskCardNumber(decryptedCardNumber);

                var decryptedExpiryDate = _cryptographyService.DecryptExpiryDate(creditCard.ExpiryDate.ToString("MM/yy"));
                maskedExpiryDate = _cryptographyService.MaskExpiryDate(decryptedExpiryDate);
            }
            catch
            {
                // If decryption fails, return masked values
            }

            return new CreditCardDTO
            {
                CreditCardId = creditCard.CreditCardId,
                MaskedCardNumber = maskedCardNumber,
                CardHolderName = creditCard.CardHolderName,
                CardType = creditCard.CardType,
                Status = creditCard.Status.ToString(),
                CreditLimit = creditCard.CreditLimit,
                CurrentBalance = creditCard.CurrentBalance,
                AvailableCredit = creditCard.AvailableCredit,
                MaskedExpiryDate = maskedExpiryDate
            };
        }

        private CreditCardApplicationResponseDTO MapToCreditCardApplicationResponseDTO(CreditCardApplication application)
        {
            return new CreditCardApplicationResponseDTO
            {
                ApplicationId = application.ApplicationId,
                Status = application.Status.ToString(),
                ApplicationDate = application.ApplicationDate,
                ApprovedDate = application.ApprovedDate,
                ApprovedLimit = application.ApprovedLimit
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
