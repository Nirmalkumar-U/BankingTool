using BankingTool.Api.Filter;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
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
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardApplicationResponseDTO>>), 200)]
        public async Task<IActionResult> GetCreditCardDashboardInitialLoad()
        {
            var result = await _creditCardService.GetCreditCardDashboardInitialLoad();
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardDTO>>), 200)]
        public async Task<IActionResult> GetCustomerCreditCards()
        {
            var result = await _creditCardService.GetCustomerCreditCards();
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 201)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 400)]
        public async Task<IActionResult> ApplyCreditCard([FromBody] CreditCardApplicationRequestDTO request)
        {
            if (request == null)
                return BadRequest(new ResponseDto<CreditCardApplicationResponseDTO> { StatuCode = 400, Status = false, Message = "Invalid request" });

            var result = await _creditCardService.ApplyCreditCard(request);
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<CreditCardDTO>), 200)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardDTO>), 404)]
        public async Task<IActionResult> GetCardDetails([FromQuery] int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<CreditCardDTO> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _creditCardService.GetCardDetails(creditCardId);
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> UpdateCardLimit([FromQuery] int creditCardId, [FromQuery] decimal newLimit)
        {
            if (creditCardId <= 0 || newLimit <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid credit card ID or new limit" });

            var result = await _creditCardService.UpdateCardLimit(creditCardId, newLimit);
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CloseCreditCard([FromQuery] int creditCardId, [FromQuery] string closureReason = "")
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _creditCardService.CloseCreditCard(creditCardId, closureReason);
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> BlockCreditCard([FromQuery] int creditCardId, [FromQuery] string blockReason = "")
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _creditCardService.BlockCreditCard(creditCardId, blockReason);
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> UnblockCreditCard([FromQuery] int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _creditCardService.UnblockCreditCard(creditCardId);
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 201)]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 400)]
        public async Task<IActionResult> RecordTransaction([FromBody] RecordCreditCardTransactionRequestDTO request)
        {
            if (request == null || request.CreditCardId <= 0 || request.Amount <= 0 ||
                !Enum.TryParse<TransactionType>(request.TransactionType, true, out var txType))
                return BadRequest(new ResponseDto<TransactionDTO> { StatuCode = 400, Status = false, Message = "Invalid parameters" });

            var result = await _transactionService.RecordTransaction(
                request.CreditCardId,
                txType,
                request.Amount,
                request.Description,
                request.MerchantName,
                request.BudgetAccounts,
                request.TransactionDate);
            return StatusCode(result.StatuCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 400)]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessCreditCardPaymentRequestDTO request)
        {
            if (request == null || request.CreditCardId <= 0 || request.PaymentAmount <= 0)
                return BadRequest(new ResponseDto<TransactionDTO> { StatuCode = 400, Status = false, Message = "Invalid parameters" });

            var result = await _transactionService.ProcessPayment(
                request.CreditCardId,
                request.PaymentAmount,
                request.Description ?? "Payment",
                request.TransactionDate,
                request.Fees);
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<TransactionDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<TransactionDTO>>), 404)]
        public async Task<IActionResult> GetTransactionHistory([FromQuery] int creditCardId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<List<TransactionDTO>> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _transactionService.GetTransactionHistory(creditCardId, pageNumber, pageSize);
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<TransactionDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<TransactionDTO>>), 400)]
        public async Task<IActionResult> GetTransactionsByDateRange([FromQuery] int creditCardId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<List<TransactionDTO>> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _transactionService.GetTransactionsByDateRange(creditCardId, fromDate, toDate);
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<StatementDTO>>), 200)]
        public async Task<IActionResult> GetStatementHistory([FromQuery] int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<List<StatementDTO>> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _statementService.GetStatementsByCardId(creditCardId);
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<StatementDTO>), 200)]
        [ProducesResponseType(typeof(ResponseDto<StatementDTO>), 404)]
        public async Task<IActionResult> GetCurrentStatement([FromQuery] int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<StatementDTO> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _statementService.GetCurrentStatement(creditCardId);
            return StatusCode(result.StatuCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<StatementDTO>), 200)]
        public async Task<IActionResult> GetStatementWithDetails([FromQuery] int statementId)
        {
            if (statementId <= 0)
                return BadRequest(new ResponseDto<StatementDTO> { StatuCode = 400, Status = false, Message = "Invalid statement ID" });

            var result = await _statementService.GetStatementWithDetails(statementId);
            return StatusCode(result.StatuCode, result);
        }
    }
}
