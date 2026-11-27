using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class CreditCardController(
        ICreditCardService creditCardService,
        ICreditCardTransactionService transactionService,
        ICreditCardStatementService statementService,
        IValidatorService validatorService,
        ICacheProvider cacheProvider,
        IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly ICreditCardService _creditCardService = creditCardService;
        private readonly ICreditCardTransactionService _transactionService = transactionService;
        private readonly ICreditCardStatementService _statementService = statementService;

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<CreditCardDashboardInitialLoadResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardDashboardInitialLoadResponse>), 400)]
        public async Task<IActionResult> GetCreditCardDashboardInitialLoad()
        {
            return await HandleRequestAsync<int?, CreditCardDashboardInitialLoadResponse>(
               null,
               null,
               () => _creditCardService.GetCreditCardDashboardInitialLoad()
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<CreditCardResponseListObject>), 200)]
        public async Task<IActionResult> GetCustomerCreditCards()
        {
            return await HandleRequestAsync<int?, CreditCardResponseListObject>(
               null,
               null,
               () => _creditCardService.GetCustomerCreditCards()
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 201)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> ApplyCreditCard([FromBody] CreditCardApplicationRequestObject request)
        {

            return await HandleRequestAsync<CreditCardApplicationRequestObject, bool>(
                request,
                ValidationRules.CreditCardApplicationRequestRules,
                () => _creditCardService.ApplyCreditCard(request.Request)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<CreditCardResponseObject>), 200)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardResponseObject>), 404)]
        public async Task<IActionResult> GetCardDetails([FromQuery] int creditCardId)
        {
            return await HandleRequestAsync<int?, CreditCardResponseObject>(
               null,
               null,
               () => _creditCardService.GetCardDetails(creditCardId)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> UpdateCardLimit([FromQuery] int creditCardId, [FromQuery] decimal newLimit)
        {
            return await HandleRequestAsync<int?, bool>(
               null,
               null,
               () => _creditCardService.UpdateCardLimit(creditCardId, newLimit)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CloseCreditCard([FromQuery] int creditCardId, [FromQuery] string closureReason = "")
        {
            return await HandleRequestAsync<int?, bool>(
               null,
               null,
               () => _creditCardService.CloseCreditCard(creditCardId, closureReason)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> BlockCreditCard([FromQuery] int creditCardId, [FromQuery] string blockReason = "")
        {
            return await HandleRequestAsync<int?, bool>(
               null,
               null,
               () => _creditCardService.BlockCreditCard(creditCardId, blockReason)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> UnblockCreditCard([FromQuery] int creditCardId)
        {
            return await HandleRequestAsync<int?, bool>(
               null,
               null,
               () => _creditCardService.UnblockCreditCard(creditCardId)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 201)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> RecordTransaction([FromBody] RecordCreditCardTransactionRequestObject request)
        {
            return await HandleRequestAsync<RecordCreditCardTransactionRequestObject, bool>(
                request,
                ValidationRules.RecordCreditCardTransactionRequestRules,
                () => _transactionService.RecordTransaction(
                request.Request.CreditCard.Id,
                request.Request.Transaction.TransactionType,
                request.Request.Transaction.Amount,
                request.Request.Transaction.Description,
                request.Request.Transaction.MerchantName,
                request.Request.BudgetAccounts,
                request.Request.Transaction.TransactionDate)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessCreditCardPaymentRequestObject request)
        {
            return await HandleRequestAsync<ProcessCreditCardPaymentRequestObject, bool>(
                request,
                ValidationRules.ProcessCreditCardPaymentRequestRules,
                () => _transactionService.ProcessPayment(
                request.Request.CreditCard.Id,
                request.Request.Transaction.PaymentAmount,
                request.Request.Transaction.Description ?? "Payment",
                request.Request.Transaction.TransactionDate,
                request.Request.Transaction.Fees)
            );  
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<TransactionResponseListObject>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TransactionResponseListObject>), 404)]
        public async Task<IActionResult> GetTransactionHistory([FromQuery] int creditCardId)
        {
            return await HandleRequestAsync<int?, TransactionResponseListObject>(
               null,
               null,
               () => _transactionService.GetTransactionHistory(creditCardId)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<StatementResponseListObject>), 200)]
        public async Task<IActionResult> GetStatementHistory([FromQuery] int creditCardId)
        {
            return await HandleRequestAsync<int?, StatementResponseListObject>(
               null,
               null,
               () => _statementService.GetStatementsByCardId(creditCardId)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<StatementResponseObject>), 200)]
        public async Task<IActionResult> GetStatementWithDetails([FromQuery] int statementId)
        {
            return await HandleRequestAsync<int?, StatementResponseObject>(
               null,
               null,
               () => _statementService.GetStatementWithDetails(statementId)
            );
        }
    }
}
