using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class ReportService(IReportRepository reportRepository) : IReportService
    {
        private readonly IReportRepository _reportRepository = reportRepository;
    }
}

