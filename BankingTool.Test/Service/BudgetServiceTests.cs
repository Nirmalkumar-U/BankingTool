using BankingTool.Repository.IRepository;
using BankingTool.Service.Service;
using Moq;

namespace BankingTool.Test.Service
{
    [TestFixture]
    public class BudgetServiceTests
    {
        private Mock<IBudgetRepository> _mockRepo;
        private BudgetService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IBudgetRepository>();
            _service = new BudgetService(_mockRepo.Object);
        }

    }
}
