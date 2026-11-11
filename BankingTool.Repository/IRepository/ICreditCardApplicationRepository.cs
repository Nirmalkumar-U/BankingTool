using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardApplicationRepository
    {
        /// <summary>
        /// Gets a credit card application by ID
        /// </summary>
        Task<CreditCardApplication> GetApplicationById(int applicationId);

        /// <summary>
        /// Gets all applications for a customer
        /// </summary>
        Task<List<CreditCardApplication>> GetApplicationsByCustomerId(int customerId);

        /// <summary>
        /// Gets applications by status
        /// </summary>
        Task<List<CreditCardApplication>> GetApplicationsByStatus(CreditCardApplicationStatus status);

        /// <summary>
        /// Gets all pending applications
        /// </summary>
        Task<List<CreditCardApplication>> GetPendingApplications();

        /// <summary>
        /// Creates a new credit card application
        /// </summary>
        Task<int> InsertApplication(CreditCardApplication application);

        /// <summary>
        /// Updates an existing application
        /// </summary>
        Task<bool> UpdateApplication(CreditCardApplication application);

        /// <summary>
        /// Approves a credit card application
        /// </summary>
        Task<bool> ApproveApplication(int applicationId, decimal approvedLimit, string approvedBy, string remarks);

        /// <summary>
        /// Rejects a credit card application
        /// </summary>
        Task<bool> RejectApplication(int applicationId, string approvalBy, string remarks);

        /// <summary>
        /// Gets all applications (admin only)
        /// </summary>
        Task<List<CreditCardApplication>> GetAllApplications();

        /// <summary>
        /// Deletes an application
        /// </summary>
        Task<bool> DeleteApplication(int applicationId);

        /// <summary>
        /// Gets the most recent application for a customer
        /// </summary>
        Task<CreditCardApplication> GetLatestApplicationByCustomerId(int customerId);
    }
}
