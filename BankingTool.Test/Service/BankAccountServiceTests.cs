using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;
using BankingTool.Service.Service;
using Moq;

namespace BankingTool.Test.Service
{
    [TestFixture]
    public class BankAccountServiceTests
    {
        private Mock<IBankAccountRepository> _bankAccountRepositoryMock;
        private Mock<IBudgetRepository> _budgetRepositoryMock;
        private IBankAccountService _bankAccountService;

        [SetUp]
        public void Setup()
        {
            _bankAccountRepositoryMock = new Mock<IBankAccountRepository>();
            _budgetRepositoryMock = new Mock<IBudgetRepository>();
            _bankAccountService = new BankAccountService(_bankAccountRepositoryMock.Object, _budgetRepositoryMock.Object);
        }

    }
}