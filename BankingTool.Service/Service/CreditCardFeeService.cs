using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    /// <summary>
    /// Service for handling credit card fees including annual fees, late payment fees, and over-limit fees
    /// </summary>
    public class CreditCardFeeService(
        ICreditCardFeeRepository feeRepository,
        ICreditCardTransactionRepository transactionRepository,
        ICreditCardRepository creditCardRepository) : ICreditCardFeeService
    {
        private readonly ICreditCardFeeRepository _feeRepository = feeRepository;
        private readonly ICreditCardTransactionRepository _transactionRepository = transactionRepository;
        private readonly ICreditCardRepository _creditCardRepository = creditCardRepository;

        public async Task<ResponseDto<bool>> ApplyAnnualFee(int creditCardId, decimal feeAmount)
        {
            return await ApplyFee(creditCardId, FeeType.Annual, feeAmount, "Annual Fee");
        }

        public async Task<ResponseDto<bool>> ApplyLatePaymentFee(int creditCardId, decimal feeAmount, int daysLate)
        {
            var reason = $"Late Payment Fee - {daysLate} days late";
            return await ApplyFee(creditCardId, FeeType.LatePay, feeAmount, reason);
        }

        public async Task<ResponseDto<bool>> ApplyOverLimitFee(int creditCardId, decimal feeAmount)
        {
            return await ApplyFee(creditCardId, FeeType.OverLimit, feeAmount, "Over Limit Fee");
        }

        public async Task<ResponseDto<bool>> ApplyTransactionFee(int creditCardId, decimal feeAmount, string reason)
        {
            return await ApplyFee(creditCardId, FeeType.TransactionFee, feeAmount, reason ?? "Transaction Fee");
        }

        public async Task<ResponseDto<bool>> ApplyCashAdvanceFee(int creditCardId, decimal feeAmount, decimal cashAdvanceAmount)
        {
            var reason = $"Cash Advance Fee on ${cashAdvanceAmount:F2}";
            return await ApplyFee(creditCardId, FeeType.CashAdvance, feeAmount, reason);
        }

        public async Task<ResponseDto<List<CreditCardFee>>> GetCardFees(int creditCardId, int pageNumber = 1, int pageSize = 10)
        {
            var response = new ResponseDto<List<CreditCardFee>>
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

                var fees = await _feeRepository.GetFeesByCardIdWithPagination(creditCardId, pageNumber, pageSize);

                response.Status = true;
                response.Message = $"Retrieved {fees.Count} fees";
                response.Response = fees;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving card fees: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<CreditCardFee>>> GetFeesByType(int creditCardId, string feeType)
        {
            var response = new ResponseDto<List<CreditCardFee>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (creditCardId <= 0 || string.IsNullOrEmpty(feeType))
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid parameters" });
                    response.StatuCode = 400;
                    return response;
                }

                var fees = await _feeRepository.GetFeesByType(creditCardId, feeType);

                response.Status = true;
                response.Message = $"Retrieved {fees.Count} fees";
                response.Response = fees;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving fees: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<decimal>> CalculateTotalFees(int creditCardId, DateTime fromDate, DateTime toDate)
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
                if (creditCardId <= 0 || fromDate > toDate)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid parameters" });
                    response.StatuCode = 400;
                    return response;
                }

                var totalFees = await _feeRepository.CalculateTotalFees(creditCardId, fromDate, toDate);

                response.Status = true;
                response.Message = "Total fees calculated successfully";
                response.Response = totalFees;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while calculating total fees: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<List<CreditCardFee>>> GetBillingCycleFees(int creditCardId, DateTime cycleStartDate, DateTime cycleEndDate)
        {
            var response = new ResponseDto<List<CreditCardFee>>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = [],
                Response = []
            };

            try
            {
                if (creditCardId <= 0 || cycleStartDate > cycleEndDate)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid parameters" });
                    response.StatuCode = 400;
                    return response;
                }

                var fees = await _feeRepository.GetFeesByBillingCycle(creditCardId, cycleStartDate, cycleEndDate);

                response.Status = true;
                response.Message = $"Retrieved {fees.Count} fees";
                response.Response = fees;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while retrieving billing cycle fees: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> ReverseFee(int feeId, string reason)
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
                if (feeId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid fee ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var fee = await _feeRepository.GetFeeById(feeId);
                if (fee == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Fee not found" });
                    response.StatuCode = 404;
                    return response;
                }

                var creditCard = await _creditCardRepository.GetCreditCardById(fee.CreditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                // Remove the fee by creating a reversal transaction
                var reverseTransaction = new CreditCardTransaction
                {
                    CreditCardId = fee.CreditCardId,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = TransactionType.Refund,
                    Amount = fee.Amount,
                    Description = $"Fee Reversal - {reason}",
                    TransactionStatus = TransactionStatus.Completed,
                    ReferenceNumber = $"FEE_REV_{fee.FeeId}",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                await _transactionRepository.InsertTransaction(reverseTransaction);

                // Update card balance
                creditCard.CurrentBalance -= fee.Amount;
                creditCard.AvailableCredit += fee.Amount;
                creditCard.ModifiedDate = DateTime.UtcNow;

                await _creditCardRepository.UpdateCreditCard(creditCard);

                // Mark fee as reversed (soft delete or status change)
                fee.FeeReason = $"Reversed - {reason}";
                await _feeRepository.UpdateFee(fee);

                response.Status = true;
                response.Message = "Fee reversed successfully";
                response.Response = true;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while reversing fee: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        public async Task<ResponseDto<bool>> WaiveFee(int feeId, string reason)
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
                if (feeId <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid fee ID" });
                    response.StatuCode = 400;
                    return response;
                }

                var fee = await _feeRepository.GetFeeById(feeId);
                if (fee == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Fee not found" });
                    response.StatuCode = 404;
                    return response;
                }

                var creditCard = await _creditCardRepository.GetCreditCardById(fee.CreditCardId);
                if (creditCard == null)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Credit card not found" });
                    response.StatuCode = 404;
                    return response;
                }

                // Remove the fee by creating a waiver transaction
                var waiverTransaction = new CreditCardTransaction
                {
                    CreditCardId = fee.CreditCardId,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = TransactionType.Refund,
                    Amount = fee.Amount,
                    Description = $"Fee Waiver - {reason}",
                    TransactionStatus = TransactionStatus.Completed,
                    ReferenceNumber = $"FEE_WAIVER_{fee.FeeId}",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                await _transactionRepository.InsertTransaction(waiverTransaction);

                // Update card balance
                creditCard.CurrentBalance -= fee.Amount;
                creditCard.AvailableCredit += fee.Amount;
                creditCard.ModifiedDate = DateTime.UtcNow;

                await _creditCardRepository.UpdateCreditCard(creditCard);

                // Mark fee as waived
                fee.FeeReason = $"Waived - {reason}";
                await _feeRepository.UpdateFee(fee);

                response.Status = true;
                response.Message = "Fee waived successfully";
                response.Response = true;
                response.StatuCode = 200;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while waiving fee: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }

        private async Task<ResponseDto<bool>> ApplyFee(int creditCardId, FeeType feeType, decimal feeAmount, string feeReason)
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
                if (creditCardId <= 0 || feeAmount <= 0)
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Invalid credit card ID or fee amount" });
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

                // Create fee record
                var fee = new CreditCardFee
                {
                    CreditCardId = creditCardId,
                    FeeType = feeType.ToString(),
                    Amount = feeAmount,
                    EffectiveDate = DateTime.UtcNow,
                    FeeReason = feeReason,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                var feeId = await _feeRepository.InsertFee(fee);

                if (feeId > 0)
                {
                    // Create a fee transaction
                    var feeTransaction = new CreditCardTransaction
                    {
                        CreditCardId = creditCardId,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionType.Fee,
                        Amount = feeAmount,
                        Description = feeReason,
                        TransactionStatus = TransactionStatus.Completed,
                        ReferenceNumber = $"FEE_{feeId}",
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow
                    };

                    await _transactionRepository.InsertTransaction(feeTransaction);

                    // Update card balance
                    creditCard.CurrentBalance += feeAmount;
                    creditCard.AvailableCredit -= feeAmount;
                    creditCard.ModifiedDate = DateTime.UtcNow;

                    await _creditCardRepository.UpdateCreditCard(creditCard);

                    response.Status = true;
                    response.Message = $"{feeType} fee applied successfully";
                    response.Response = true;
                    response.StatuCode = 201;
                }
                else
                {
                    response.Errors.Add(new Errors { ErrorMessage = "Failed to apply fee" });
                    response.StatuCode = 500;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Errors { ErrorMessage = $"An error occurred while applying fee: {ex.Message}" });
                response.StatuCode = 500;
            }

            return response;
        }
    }
}
