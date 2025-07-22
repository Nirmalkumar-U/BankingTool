using BankingTool.Model.Dto.RequestDto.Budget;
using BankingTool.Model.Model;
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

        [Test]
        public async Task CreateBudgetAccount_Should_Return_Success()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetLastBudgetAccount()).ReturnsAsync(new BudgetAccount { BudgetNumber = "60000001" });
            _mockRepo.Setup(r => r.GetPrimaryAccountIdOfCustomer()).ReturnsAsync(1);
            _mockRepo.Setup(r => r.CreateBudgetAccount(It.IsAny<BudgetAccount>(), It.IsAny<BudgetTransaction>(), It.IsAny<BudgetTransactionDetail>()))
                     .ReturnsAsync(true);

            // Act
            var result = await _service.CreateBudgetAccount("Test Budget", 1000);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(200));
            Assert.That(result.Message, Is.EqualTo("Budget Account Created Successfully"));
        }

        [Test]
        public async Task CreateBudgetAccount_Should_Return_Failure()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetLastBudgetAccount()).ReturnsAsync((BudgetAccount)null);
            _mockRepo.Setup(r => r.GetPrimaryAccountIdOfCustomer()).ReturnsAsync(1);
            _mockRepo.Setup(r => r.CreateBudgetAccount(It.IsAny<BudgetAccount>(), It.IsAny<BudgetTransaction>(), It.IsAny<BudgetTransactionDetail>()))
                     .ReturnsAsync(false);

            // Act
            var result = await _service.CreateBudgetAccount("Fail Budget", 1000);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(400));
            Assert.That(result.Errors.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AcceptBudgetTransaction_Should_Return_ValidationError_When_Null()
        {
            // Arrange
            List<AcceptBudgetTransactionRequestBudgetAcccount> budgetAccounts = null;

            // Act
            var result = await _service.AcceptBudgetTransaction(budgetAccounts, 1);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(400));
            Assert.That(result.ValidationErrors.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AcceptBudgetTransaction_Should_Return_ValidationError_When_Amount_Exceeds()
        {
            // Arrange
            var budgetAccounts = new List<AcceptBudgetTransactionRequestBudgetAcccount>
            {
                new() { Amount = 500 }
            };

            _mockRepo.Setup(r => r.GetBudgetNotification(It.IsAny<int>()))
                     .ReturnsAsync(new BudgetNotification { Amount = 100 });

            // Act
            var result = await _service.AcceptBudgetTransaction(budgetAccounts, 1);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(400));
            Assert.That(result.ValidationErrors.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AcceptBudgetTransaction_Should_Return_Success()
        {
            // Arrange
            var budgetAccounts = new List<AcceptBudgetTransactionRequestBudgetAcccount>
            {
                new() { Amount = 50 }
            };

            _mockRepo.Setup(r => r.GetBudgetNotification(It.IsAny<int>()))
                     .ReturnsAsync(new BudgetNotification { Amount = 100 });

            _mockRepo.Setup(r => r.AcceptBudgetTransaction(budgetAccounts, 1)).ReturnsAsync(true);

            // Act
            var result = await _service.AcceptBudgetTransaction(budgetAccounts, 1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(200));
        }

        [Test]
        public async Task RejectBudgetTransaction_Should_Return_Success()
        {
            // Arrange
            _mockRepo.Setup(r => r.RejectBudgetTransaction(1)).ReturnsAsync(true);

            // Act
            var result = await _service.RejectBudgetTransaction(1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(200));
        }

        [Test]
        public async Task RejectBudgetTransaction_Should_Return_Failure()
        {
            // Arrange
            _mockRepo.Setup(r => r.RejectBudgetTransaction(1)).ReturnsAsync(false);

            // Act
            var result = await _service.RejectBudgetTransaction(1);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.That(result.StatuCode, Is.EqualTo(400));
            Assert.That(result.Errors.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ChangeBudgetAccountStatus_Should_Return_Success()
        {
            // Arrange
            _mockRepo.Setup(r => r.ChangeBudgetAccountStatus(1)).ReturnsAsync(true);

            // Act
            var result = await _service.ChangeBudgetAccountStatus(1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
        }
    }
}
