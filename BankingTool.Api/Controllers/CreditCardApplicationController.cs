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
    public class CreditCardApplicationController(
        ICreditCardService creditCardService,
        IValidatorService validatorService,
        ICacheProvider cacheProvider,
        IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly ICreditCardService _creditCardService = creditCardService;

        /// <summary>
        /// Apply for a new credit card
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 201)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 400)]
        public async Task<IActionResult> CreateApplication([FromBody] CreditCardApplicationRequestDTO request)
        {
            if (request == null)
                return BadRequest(new ResponseDto<CreditCardApplicationResponseDTO> 
                { 
                    StatuCode = 400, 
                    Status = false, 
                    Message = "Invalid request" 
                });

            var result = await _creditCardService.ApplyCreditCard(request);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get application status by application ID
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 200)]
        [ProducesResponseType(typeof(ResponseDto<CreditCardApplicationResponseDTO>), 404)]
        public async Task<IActionResult> GetApplicationStatus([FromQuery] int applicationId)
        {
            if (applicationId <= 0)
                return BadRequest(new ResponseDto<CreditCardApplicationResponseDTO> 
                { 
                    StatuCode = 400, 
                    Status = false, 
                    Message = "Invalid application ID" 
                });

            var result = await _creditCardService.GetApplicationStatus(applicationId);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Get all applications for a customer
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardApplicationResponseDTO>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<CreditCardApplicationResponseDTO>>), 404)]
        public async Task<IActionResult> GetApplicationHistory([FromQuery] int customerId)
        {
            if (customerId <= 0)
                return BadRequest(new ResponseDto<List<CreditCardApplicationResponseDTO>> 
                { 
                    StatuCode = 400, 
                    Status = false, 
                    Message = "Invalid customer ID" 
                });

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
        /// Approve a credit card application (Admin/Officer only)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> ApproveApplication([FromQuery] int applicationId, [FromQuery] decimal approvedLimit, [FromQuery] string approvalRemarks = "")
        {
            if (applicationId <= 0 || approvedLimit <= 0)
                return BadRequest(new ResponseDto<bool> 
                { 
                    StatuCode = 400, 
                    Status = false, 
                    Message = "Invalid application ID or approved limit" 
                });

            var result = await _creditCardService.ApproveCreditCard(applicationId, approvedLimit, approvalRemarks);
            return StatusCode(result.StatuCode, result);
        }

        /// <summary>
        /// Reject a credit card application (Admin/Officer only)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> RejectApplication([FromQuery] int applicationId, [FromQuery] string rejectionRemarks = "")
        {
            if (applicationId <= 0)
                return BadRequest(new ResponseDto<bool> 
                { 
                    StatuCode = 400, 
                    Status = false, 
                    Message = "Invalid application ID" 
                });

            var result = await _creditCardService.RejectCreditCard(applicationId, rejectionRemarks);
            return StatusCode(result.StatuCode, result);
        }
    }
}
