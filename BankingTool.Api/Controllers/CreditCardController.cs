using BankingTool.Api.Filter;
using BankingTool.Model;
using BankingTool.Model.Enums;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.Response;
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
        ICreditCardFeeService feeService,
        IValidatorService validatorService,
        ICacheProvider cacheProvider,
        IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly ICreditCardService _creditCardService = creditCardService;
        private readonly ICreditCardTransactionService _transactionService = transactionService;
        private readonly ICreditCardStatementService _statementService = statementService;
        private readonly ICreditCardFeeService _feeService = feeService;

        /// <summary>
        /// Apply for a new credit card
        /// </summary>
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

        /// <summary>
        /// Approve a credit card application (Admin/Officer only)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> ApproveCreditCard([FromQuery] int applicationId, [FromQuery] decimal approvedLimit, [FromQuery] string approvalRemarks = "")
        {
            if (applicationId <= 0 || approvedLimit <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid application ID or approved limit" });

            var result = await _creditCardService.ApproveCreditCard(applicationId, approvedLimit, approvalRemarks);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Reject a credit card application (Admin/Officer only)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> RejectCreditCard([FromQuery] int applicationId, [FromQuery] string rejectionRemarks = "")
        {
            if (applicationId <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid application ID" });

            var result = await _creditCardService.RejectCreditCard(applicationId, rejectionRemarks);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get credit card details with masked sensitive data
        /// </summary>
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

        /// <summary>
        /// Get all active credit cards for a customer
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardDTO>>), 404)]
        public async Task<IActionResult> GetCustomerCreditCards()
        {
            var result = await _creditCardService.GetCustomerCreditCards();
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Update credit card limit (Admin/Officer only)
        /// </summary>
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

        /// <summary>
        /// Close a credit card
        /// </summary>
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

        /// <summary>
        /// Block a credit card (temporary suspension)
        /// </summary>
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

        /// <summary>
        /// Unblock a credit card
        /// </summary>
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

        /// <summary>
        /// Get application status
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 200)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 404)]
        public async Task<IActionResult> GetApplicationStatus([FromQuery] int applicationId)
        {
            if (applicationId <= 0)
                return BadRequest(new ResponseDto<CreditCardApplicationResponseDTO> { StatuCode = 400, Status = false, Message = "Invalid application ID" });

            var result = await _creditCardService.GetApplicationStatus(applicationId);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get customer's applications
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardApplicationResponseDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardApplicationResponseDTO>>), 404)]
        public async Task<IActionResult> GetCustomerApplications([FromQuery] int customerId)
        {
            if (customerId <= 0)
                return BadRequest(new ResponseDto<List<CreditCardApplicationResponseDTO>> { StatuCode = 400, Status = false, Message = "Invalid customer ID" });

            var result = await _creditCardService.GetCustomerApplications(customerId);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get all pending applications (Admin/Officer only)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardApplicationResponseDTO>>), 200)]
        public async Task<IActionResult> GetPendingApplications()
        {
            var result = await _creditCardService.GetPendingApplications();
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Record a transaction (purchase)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 201)]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 400)]
        public async Task<IActionResult> RecordTransaction([FromQuery] int creditCardId, [FromQuery] string transactionType, 
            [FromQuery] decimal amount, [FromQuery] string description, [FromQuery] string merchantName = "", [FromQuery] string referenceNumber = "")
        {
            if (creditCardId <= 0 || amount <= 0 || !Enum.TryParse<TransactionType>(transactionType, out var txType))
                return BadRequest(new ResponseDto<TransactionDTO> { StatuCode = 400, Status = false, Message = "Invalid parameters" });

            var result = await _transactionService.RecordTransaction(creditCardId, txType, amount, description, merchantName, referenceNumber);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Process a payment
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TransactionDTO>), 400)]
        public async Task<IActionResult> ProcessPayment([FromQuery] int creditCardId, [FromQuery] decimal paymentAmount, [FromQuery] string description = "Payment")
        {
            if (creditCardId <= 0 || paymentAmount <= 0)
                return BadRequest(new ResponseDto<TransactionDTO> { StatuCode = 400, Status = false, Message = "Invalid parameters" });

            var result = await _transactionService.ProcessPayment(creditCardId, paymentAmount, description);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get transaction history
        /// </summary>
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

        /// <summary>
        /// Get transactions by date range
        /// </summary>
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

        /// <summary>
        /// Get recent transactions
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<TransactionDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<TransactionDTO>>), 404)]
        public async Task<IActionResult> GetRecentTransactions([FromQuery] int creditCardId, [FromQuery] int count = 10)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<List<TransactionDTO>> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _transactionService.GetRecentTransactions(creditCardId, count);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Reverse a transaction
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> ReverseTransaction([FromQuery] int transactionId, [FromQuery] string reason = "")
        {
            if (transactionId <= 0)
                return BadRequest(new ResponseDto<bool> { StatuCode = 400, Status = false, Message = "Invalid transaction ID" });

            var result = await _transactionService.ReverseTransaction(transactionId, reason);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get current statement
        /// </summary>
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

        /// <summary>
        /// Get statement history
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<StatementDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<StatementDTO>>), 404)]
        public async Task<IActionResult> GetStatementHistory([FromQuery] int creditCardId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<List<StatementDTO>> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _statementService.GetStatementHistory(creditCardId, pageNumber, pageSize);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get overdue statements
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<StatementDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<StatementDTO>>), 404)]
        public async Task<IActionResult> GetOverdueStatements([FromQuery] int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<List<StatementDTO>> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _statementService.GetOverdueStatements(creditCardId);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get available credit
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<decimal>), 200)]
        [ProducesResponseType(typeof(ResponseDto<decimal>), 404)]
        public async Task<IActionResult> GetAvailableCredit([FromQuery] int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest(new ResponseDto<decimal> { StatuCode = 400, Status = false, Message = "Invalid credit card ID" });

            var result = await _transactionService.CalculateAvailableCredit(creditCardId);
            return StatusCode(result.StatuCode, result);
        }
    }
}
