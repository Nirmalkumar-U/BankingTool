using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Dto.SPDto;
using BankingTool.Model.Model;
using BankingTool.Repository;
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
        private const int MinimumBalance = 1000;
        private const int DefaultCardLimit = 5000;

        [SetUp]
        public void Setup()
        {
            _bankAccountRepositoryMock = new Mock<IBankAccountRepository>();
            _budgetRepositoryMock = new Mock<IBudgetRepository>();
            _bankAccountService = new BankAccountService(_bankAccountRepositoryMock.Object, _budgetRepositoryMock.Object);
        }

        #region GetDashBoardInitialLoad Tests
        [Test]
        public async Task GetDashBoardInitialLoad_ReturnsCorrectResponse_WhenAccountsExist()
        {
            // Arrange
            var accountList = new List<DashBoardAccountList>
            {
                new DashBoardAccountList { AccountType = "Saving Account", Balance = 1000 },
                new DashBoardAccountList { AccountType = "Saving Account", Balance = 2000 },
                new DashBoardAccountList { AccountType = "Checking Account", Balance = 1500 }
            };
            _bankAccountRepositoryMock.Setup(x => x.GetDashBoardInitialLoad()).ReturnsAsync(accountList);

            // Act
            var result = await _bankAccountService.GetDashBoardInitialLoad();

            // Assert
            Assert.IsTrue(result.Status);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(3000, result.Response.TotalBalance); // Sum of Saving Account balances
            Assert.AreEqual(accountList, result.Response.DashBoardAccountList);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            Assert.IsEmpty(result.DropDownList);
        }

        [Test]
        public async Task GetDashBoardInitialLoad_ReturnsCorrectResponse_WhenNoSavingAccounts()
        {
            // Arrange
            var accountList = new List<DashBoardAccountList>
            {
                new DashBoardAccountList { AccountType = "Checking Account", Balance = 1500 }
            };
            _bankAccountRepositoryMock.Setup(x => x.GetDashBoardInitialLoad()).ReturnsAsync(accountList);

            // Act
            var result = await _bankAccountService.GetDashBoardInitialLoad();

            // Assert
            Assert.IsTrue(result.Status);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(0, result.Response.TotalBalance); // No Saving Accounts
            Assert.AreEqual(accountList, result.Response.DashBoardAccountList);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            Assert.IsEmpty(result.DropDownList);
        }
        #endregion

        #region GetCreateAccountInitialLoad Tests
        [Test]
        public async Task GetCreateAccountInitialLoad_ReturnsCorrectResponse()
        {
            // Arrange
            var accountTypeDropDown = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Savings" } };
            var customerDropDown = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "John Doe" } };
            _bankAccountRepositoryMock.Setup(x => x.GetAccountTypeDropDown()).ReturnsAsync(accountTypeDropDown);
            _bankAccountRepositoryMock.Setup(x => x.GetAllActiveCustomerDropDown()).ReturnsAsync(customerDropDown);

            // Act
            var result = await _bankAccountService.GetCreateAccountInitialLoad();

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            Assert.AreEqual(2, result.DropDownList.Count);
            Assert.AreEqual("AccountType", result.DropDownList[0].Name);
            Assert.AreEqual(accountTypeDropDown, result.DropDownList[0].DropDown);
            Assert.AreEqual("Customer", result.DropDownList[1].Name);
            Assert.AreEqual(customerDropDown, result.DropDownList[1].DropDown);
        }
        #endregion

        #region GetBankDetailsDropDownWithoutCustomerAndAccountType Tests
        [Test]
        public async Task GetBankDetailsDropDownWithoutCustomerAndAccountType_ReturnsCorrectResponse()
        {
            // Arrange
            var bankDropDown = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Bank A" } };
            _bankAccountRepositoryMock.Setup(x => x.GetBankDetailsDropDownWithoutCustomerAndAccountType(1, 1)).ReturnsAsync(bankDropDown);

            // Act
            var result = await _bankAccountService.GetBankDetailsDropDownWithoutCustomerAndAccountType(1, 1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            Assert.AreEqual(1, result.DropDownList.Count);
            Assert.AreEqual("Bank", result.DropDownList[0].Name);
            Assert.AreEqual(bankDropDown, result.DropDownList[0].DropDown);
        }
        #endregion

        #region IsCustomerHasCreditCardInThatBank Tests
        [Test]
        public async Task IsCustomerHasCreditCardInThatBank_ReturnsTrue_WhenCustomerHasCreditCard()
        {
            // Arrange
            _bankAccountRepositoryMock.Setup(x => x.IsCustomerHasCreditCardInThatBank(1, 1)).ReturnsAsync(true);

            // Act
            var result = await _bankAccountService.IsCustomerHasCreditCardInThatBank(1, 1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }

        [Test]
        public async Task IsCustomerHasCreditCardInThatBank_ReturnsFalse_WhenCustomerHasNoCreditCard()
        {
            // Arrange
            _bankAccountRepositoryMock.Setup(x => x.IsCustomerHasCreditCardInThatBank(1, 1)).ReturnsAsync(false);

            // Act
            var result = await _bankAccountService.IsCustomerHasCreditCardInThatBank(1, 1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region CreateAccount Tests
        [Test]
        public async Task CreateAccount_ReturnsFailure_WhenModelIsNull()
        {
            // Act
            var result = await _bankAccountService.CreateAccount(null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(0, result.StatuCode);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }

        [Test]
        public async Task CreateAccount_CreatesAccountSuccessfully_WhenCustomerHasNoAccountsAndNoCreditCard()
        {
            // Arrange
            var model = new CreateAccountRequest
            {
                Customer = new CreateAccountRequestCustomer { Id = 1, CustomerWantCreditCard = false },
                Account = new CreateAccountRequestAccount { AccountTypeId = 1, DoYouWantToChangeThisAccountToPrimaryAccount = false },
                Bank = new CreateAccountRequestBank { Id = 1 }
            };
            //_bankAccountRepositoryMock.Setup(x => x.GetNewAccountNumber()).ReturnsAsync("9000000000000001");
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.DebitCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.IsAnyAccountForThisCustomer(1)).ReturnsAsync(false);
            _bankAccountRepositoryMock.Setup(x => x.GetCustomerByCustomerId(1)).ReturnsAsync(new Customer { CustomerId = 1 });
            _bankAccountRepositoryMock.Setup(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.IsAny<CreditScore>(), It.IsAny<Customer>(), false, false, false)).Returns(true);

            // Act
            var result = await _bankAccountService.CreateAccount(model);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual("Account Created Successfully...", result.Message);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            _bankAccountRepositoryMock.Verify(x => x.CreateAccount(It.Is<Account>(a => a.AccountNumber == "9000000000000001" && a.Balance == MinimumBalance), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.Is<Card>(c => c.CardType == CardType.DebitCard), null, It.IsAny<CreditScore>(), It.IsAny<Customer>(), false, false, false), Times.Once());
        }

        [Test]
        public async Task CreateAccount_CreatesAccountSuccessfully_WhenCustomerHasAccountsAndWantsCreditCard()
        {
            // Arrange
            var model = new CreateAccountRequest
            {
                Customer = new CreateAccountRequestCustomer { Id = 1, CustomerWantCreditCard = true },
                Account = new CreateAccountRequestAccount { AccountTypeId = 1, DoYouWantToChangeThisAccountToPrimaryAccount = true },
                Bank = new CreateAccountRequestBank { Id = 1 }
            };
            var customer = new Customer { CustomerId = 1, PrimaryAccountNumber = "9000000000000000" };
            var creditScore = new CreditScore { CreditScoreValue = 600 };
            //_bankAccountRepositoryMock.Setup(x => x.GetNewAccountNumber()).ReturnsAsync("9000000000000001");
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.DebitCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.CreditCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.IsAnyAccountForThisCustomer(1)).ReturnsAsync(true);
            _bankAccountRepositoryMock.Setup(x => x.GetCustomerByCustomerId(1)).ReturnsAsync(customer);
            _bankAccountRepositoryMock.Setup(x => x.GetCreditScoreOfCustomer(1)).ReturnsAsync(creditScore);
            _bankAccountRepositoryMock.Setup(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), It.IsAny<Card>(), It.IsAny<CreditScore>(), It.IsAny<Customer>(), true, true, true)).Returns(true);

            // Act
            var result = await _bankAccountService.CreateAccount(model);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual("Account Created Successfully...", result.Message);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            _bankAccountRepositoryMock.Verify(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), It.Is<Card>(c => c.CardType == CardType.CreditCard), It.IsAny<CreditScore>(), It.Is<Customer>(c => c.PrimaryAccountNumber == "9000000000000001"), true, true, true), Times.Once());
        }

        [Test]
        public async Task CreateAccount_ReturnsFailure_WhenRepositoryFails()
        {
            // Arrange
            var model = new CreateAccountRequest
            {
                Customer = new CreateAccountRequestCustomer { Id = 1, CustomerWantCreditCard = false },
                Account = new CreateAccountRequestAccount { AccountTypeId = 1, DoYouWantToChangeThisAccountToPrimaryAccount = false },
                Bank = new CreateAccountRequestBank { Id = 1 }
            };
            //_bankAccountRepositoryMock.Setup(x => x.GetNewAccountNumber()).ReturnsAsync("9000000000000001");
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.DebitCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.IsAnyAccountForThisCustomer(1)).ReturnsAsync(false);
            _bankAccountRepositoryMock.Setup(x => x.GetCustomerByCustomerId(1)).ReturnsAsync(new Customer { CustomerId = 1 });
            _bankAccountRepositoryMock.Setup(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.IsAny<CreditScore>(), It.IsAny<Customer>(), false, false, false)).Returns(false);

            // Act
            var result = await _bankAccountService.CreateAccount(model);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.AreEqual("Failed to create account...", result.Message);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("Create Account", result.Errors[0].PropertyName);
            Assert.AreEqual("Failed to create account", result.Errors[0].ErrorMessage);
        }
        #endregion

        #region TransactionsListForCustomer Tests
        [Test]
        public async Task TransactionsListForCustomer_ReturnsCorrectResponse()
        {
            // Arrange
            var accountInfo = new GetTransactionsListResponseAccountInfo();
            var cardInfo = new List<GetTransactionsListResponseCardInfo>();
            var transactions = new List<GetTransactionsListResponseTransactionList>();
            var senderAccounts = new List<GetSenderAccount> { new GetSenderAccount { AccountId = 1, AccountDescription = "Account 1" } };
            var receiverAccounts = new List<GetReceiverAccount> { new GetReceiverAccount { AccountId = 2, AccountDescription = "Account 2" } };
            var transactionCategories = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Transfer" } };
            var transactionTags = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Tag1" } };
            _bankAccountRepositoryMock.Setup(x => x.GetAccountIdByBankIdAndAccountTypeAndCustomerId(1, 1, 1)).ReturnsAsync((1, "John Doe", "Bank A", "Savings"));
            _bankAccountRepositoryMock.Setup(x => x.GetSenderAccountDropdown(1)).ReturnsAsync(senderAccounts);
            _bankAccountRepositoryMock.Setup(x => x.GetReceiverAccountDropdown(1)).ReturnsAsync(receiverAccounts);
            _bankAccountRepositoryMock.Setup(x => x.GetAccountInfo(1, "Savings", "John Doe", "Bank A")).ReturnsAsync(accountInfo);
            _bankAccountRepositoryMock.Setup(x => x.GetCardInfoByAccountId(1, "John Doe", "Bank A")).ReturnsAsync(cardInfo);
            _bankAccountRepositoryMock.Setup(x => x.GetTransactionByAccountId(1, null, null, null, null, null, null)).ReturnsAsync(transactions);
            _bankAccountRepositoryMock.Setup(x => x.GetAllTransactionCategory()).ReturnsAsync(transactionCategories);
            _bankAccountRepositoryMock.Setup(x => x.GetAllTransactionTag()).ReturnsAsync(transactionTags);

            // Act
            var result = await _bankAccountService.TransactionsListForCustomer(1, 1, 1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(accountInfo, result.Response.AccountInfo);
            Assert.AreEqual(cardInfo, result.Response.CardInfo);
            Assert.AreEqual(transactions, result.Response.TransactionsList);
            Assert.AreEqual(4, result.DropDownList.Count);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region TransactionsList Tests
        [Test]
        public async Task TransactionsList_ReturnsCorrectResponse()
        {
            // Arrange
            var request = new TransactionListRequest
            {
                Account = new TransactionListRequestAccount { Id = 1, SenderAccountId = 2, ReceiverAccountId = 3 },
                Transaction = new TransactionListRequestTransaction { TransactionTag = "Tag1", TransactionFromDate = DateTime.Now, TransactionToDate = DateTime.Now, TransactionCategory = "Transfer" }
            };
            var transactions = new List<GetTransactionsListResponseTransactionList>();
            _bankAccountRepositoryMock.Setup(x => x.GetTransactionByAccountId(1, "Tag1", It.IsAny<DateTime>(), It.IsAny<DateTime>(), 2, 3, "Transfer")).ReturnsAsync(transactions);

            // Act
            var result = await _bankAccountService.TransactionsList(request);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(transactions, result.Response.TransactionsList);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            Assert.IsEmpty(result.DropDownList);
        }
        #endregion

        #region GetAccountTypeDropDownListByCustomerIdAndBankId Tests
        [Test]
        public async Task GetAccountTypeDropDownListByCustomerIdAndBankId_ReturnsCorrectResponse()
        {
            // Arrange
            var accountTypes = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Savings" } };
            _bankAccountRepositoryMock.Setup(x => x.GetAccountTypeDropDownListByCustomerIdAndBankId(1, 1)).ReturnsAsync(accountTypes);

            // Act
            var result = await _bankAccountService.GetAccountTypeDropDownListByCustomerIdAndBankId(1, 1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(1, result.DropDownList.Count);
            Assert.AreEqual("AccountType", result.DropDownList[0].Name);
            Assert.AreEqual(accountTypes, result.DropDownList[0].DropDown);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region BankDropDownList Tests
        [Test]
        public async Task BankDropDownList_ReturnsCorrectResponse()
        {
            // Arrange
            var banks = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Bank A" } };
            _bankAccountRepositoryMock.Setup(x => x.GetBankDropDownListByCustomerId()).ReturnsAsync(banks);

            // Act
            var result = await _bankAccountService.BankDropDownList();

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(1, result.DropDownList.Count);
            Assert.AreEqual("Bank", result.DropDownList[0].Name);
            Assert.AreEqual(banks, result.DropDownList[0].DropDown);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region GetTransferAmountInitialLoad Tests
        [Test]
        public async Task GetTransferAmountInitialLoad_ReturnsCorrectResponse()
        {
            // Arrange
            var fromAccounts = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Account 1" } };
            var toAccounts = new List<DropDownDto> { new DropDownDto { Key = 2, Value = "Account 2" } };
            var transactionTags = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Tag1" } };
            var budgetAccounts = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Budget 1" } };
            _bankAccountRepositoryMock.Setup(x => x.GetFromAccountListByCustomerId()).ReturnsAsync(fromAccounts);
            _bankAccountRepositoryMock.Setup(x => x.GetToAccountListOnWithoutCustomerId()).ReturnsAsync(toAccounts);
            _bankAccountRepositoryMock.Setup(x => x.GetAllTransactionTag()).ReturnsAsync(transactionTags);
            _budgetRepositoryMock.Setup(x => x.GetBudgetAccountDropdown()).ReturnsAsync(budgetAccounts);

            // Act
            var result = await _bankAccountService.GetTransferAmountInitialLoad();

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(4, result.DropDownList.Count);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region GetSelfTransferInitialLoad Tests
        [Test]
        public async Task GetSelfTransferInitialLoad_ReturnsCorrectResponse()
        {
            // Arrange
            var fromAccounts = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Account 1" } };
            var transactionTags = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Tag1" } };
            var budgetAccounts = new List<DropDownDto> { new DropDownDto { Key = 1, Value = "Budget 1" } };
            _bankAccountRepositoryMock.Setup(x => x.GetFromAccountListByCustomerId()).ReturnsAsync(fromAccounts);
            _bankAccountRepositoryMock.Setup(x => x.GetAllTransactionTag()).ReturnsAsync(transactionTags);
            _budgetRepositoryMock.Setup(x => x.GetBudgetAccountDropdown()).ReturnsAsync(budgetAccounts);

            // Act
            var result = await _bankAccountService.GetSelfTransferInitialLoad();

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(3, result.DropDownList.Count);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region GetToAccountListExcludedByAccountId Tests
        [Test]
        public async Task GetToAccountListExcludedByAccountId_ReturnsCorrectResponse()
        {
            // Arrange
            var toAccounts = new List<DropDownDto> { new DropDownDto { Key = 2, Value = "Account 2" } };
            _bankAccountRepositoryMock.Setup(x => x.GetToAccountListExcludedByAccountId(1)).ReturnsAsync(toAccounts);

            // Act
            var result = await _bankAccountService.GetToAccountListExcludedByAccountId(1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual(200, result.StatuCode);
            Assert.AreEqual(1, result.DropDownList.Count);
            Assert.AreEqual("ToAccount", result.DropDownList[0].Name);
            Assert.AreEqual(toAccounts, result.DropDownList[0].DropDown);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region GetAccountBalance Tests
        [Test]
        public async Task GetAccountBalance_ReturnsCorrectBalance()
        {
            // Arrange
            _bankAccountRepositoryMock.Setup(x => x.GetAccountBalance(1)).ReturnsAsync(1000);

            // Act
            var result = await _bankAccountService.GetAccountBalance(1);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.AreEqual(1000, result.Response);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
        }
        #endregion

        #region TransferAmount Tests
        [Test]
        public async Task TransferAmount_ReturnsFailure_WhenBalanceIsInsufficient()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 500, CustomerId = 1 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);

            // Act
            var result = await _bankAccountService.TransferAmount(1, 2, 1000, "Transfer", DateTime.Now, "Tag1", null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.Account.Amount", result.ValidationErrors[0].PropertyPath);
        }

        [Test]
        public async Task TransferAmount_ReturnsFailure_WhenBudgetAccountAmountExceedsTransferAmount()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 2000, CustomerId = 1 };
            var budgetAccounts = new List<TransferAmountRequestBudgetAccount> { new TransferAmountRequestBudgetAccount { Id = 1, Amount = 1500 } };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);

            // Act
            var result = await _bankAccountService.TransferAmount(1, 2, 1000, "Transfer", DateTime.Now, "Tag1", budgetAccounts);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.BudgetAccount.Asmount", result.ValidationErrors[0].PropertyPath);
        }

        [Test]
        public async Task TransferAmount_ReturnsFailure_WhenTransactionDateIsBeforeLastTransaction()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 2000, CustomerId = 1 };
            var lastTransaction = new Transaction { TransactionTime = DateTime.Now.AddDays(1) };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(1)).ReturnsAsync(lastTransaction);

            // Act
            var result = await _bankAccountService.TransferAmount(1, 2, 500, "Transfer", DateTime.Now, "Tag1", null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.IsNotEmpty(result.Errors);
            Assert.AreEqual("TransferAmount", result.Errors[0].PropertyName);
        }

        [Test]
        public async Task TransferAmount_Succeeds_WithBudgetAccountsAndNotification()
        {
            // Arrange
            var fromAccount = new Account { AccountId = 1, Balance = 2000, CustomerId = 1 };
            var toAccount = new Account { AccountId = 2, Balance = 1000, CustomerId = 2 };
            var budgetAccounts = new List<TransferAmountRequestBudgetAccount> { new TransferAmountRequestBudgetAccount { Id = 1, Amount = 500 } };
            var budgetAccount = new BudgetAccount { BudgetAccountId = 1, Balance = 1000 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(fromAccount);
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(2)).ReturnsAsync(toAccount);
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(1)).ReturnsAsync((Transaction)null);
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(2)).ReturnsAsync((Transaction)null);
            _budgetRepositoryMock.Setup(x => x.GetLastBudgetTransaction(1)).ReturnsAsync((BudgetTransaction)null);
            _budgetRepositoryMock.Setup(x => x.GetBudgetAccountById(1)).ReturnsAsync(budgetAccount);
            _budgetRepositoryMock.Setup(x => x.IsCustomerAsBudgetAccount(2)).ReturnsAsync(true);
            _bankAccountRepositoryMock.Setup(x => x.TransferAmount(It.IsAny<Account>(), It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<TransactionDetail>(), It.IsAny<List<BudgetTransactionDto>>(), It.IsAny<BudgetNotification>(), true)).Returns(true);

            // Act
            var result = await _bankAccountService.TransferAmount(1, 2, 1000, "Transfer", DateTime.Now, "Tag1", budgetAccounts);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.AreEqual("Transfered Successfully.", result.Message);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            _bankAccountRepositoryMock.Verify(x => x.TransferAmount(It.Is<Account>(a => a.Balance == 1000), It.Is<Account>(a => a.Balance == 2000), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<TransactionDetail>(), It.IsAny<List<BudgetTransactionDto>>(), It.IsAny<BudgetNotification>(), true), Times.Once());
        }
        #endregion

        #region DepositAmount Tests
        [Test]
        public async Task DepositAmount_ReturnsFailure_WhenBudgetAccountAmountExceedsDepositAmount()
        {
            // Arrange
            var budgetAccounts = new List<DepositAmountRequestBudgetAccount> { new DepositAmountRequestBudgetAccount { Id = 1, Amount = 1500 } };

            // Act
            var result = await _bankAccountService.DepositAmount(1, 1000, "Deposit", DateTime.Now, budgetAccounts);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.BudgetAccount.Asmount", result.ValidationErrors[0].PropertyPath);
        }

        [Test]
        public async Task DepositAmount_ReturnsFailure_WhenTransactionDateIsBeforeLastTransaction()
        {
            // Arrange
            var lastTransaction = new Transaction { TransactionTime = DateTime.Now.AddDays(1) };
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(1)).ReturnsAsync(lastTransaction);

            // Act
            var result = await _bankAccountService.DepositAmount(1, 1000, "Deposit", DateTime.Now, null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.IsNotEmpty(result.Errors);
            Assert.AreEqual("DepositAmount", result.Errors[0].PropertyName);
        }

        [Test]
        public async Task DepositAmount_Succeeds_WithBudgetAccounts()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 1000 };
            var budgetAccounts = new List<DepositAmountRequestBudgetAccount> { new DepositAmountRequestBudgetAccount { Id = 1, Amount = 500 } };
            var budgetAccount = new BudgetAccount { BudgetAccountId = 1, Balance = 1000 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(1)).ReturnsAsync((Transaction)null);
            _budgetRepositoryMock.Setup(x => x.GetLastBudgetTransaction(1)).ReturnsAsync((BudgetTransaction)null);
            _budgetRepositoryMock.Setup(x => x.GetBudgetAccountById(1)).ReturnsAsync(budgetAccount);

            // Act
            var result = await _bankAccountService.DepositAmount(1, 1000, "Deposit", DateTime.Now, budgetAccounts);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            _bankAccountRepositoryMock.Verify(x => x.CashTransfer(It.Is<Account>(a => a.Balance == 2000), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), true, It.IsAny<List<BudgetTransactionDto>>()), Times.Once());
        }
        #endregion

        #region CashWithdraw Tests
        [Test]
        public async Task CashWithdraw_ReturnsFailure_WhenBalanceIsInsufficient()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 500 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);

            // Act
            var result = await _bankAccountService.CashWithdraw(1, 1000, "Withdraw", DateTime.Now, null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.Account.Amount", result.ValidationErrors[0].PropertyPath);
        }

        [Test]
        public async Task CashWithdraw_ReturnsFailure_WhenBudgetAccountAmountExceedsWithdrawAmount()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 2000 };
            var budgetAccounts = new List<CashWithdrawRequestBudgetAccount> { new CashWithdrawRequestBudgetAccount { Id = 1, Amount = 1500 } };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);

            // Act
            var result = await _bankAccountService.CashWithdraw(1, 1000, "Withdraw", DateTime.Now, budgetAccounts);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.AreEqual(400, result.StatuCode);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.BudgetAccount.Asmount", result.ValidationErrors[0].PropertyPath);
        }

        [Test]
        public async Task CashWithdraw_ReturnsFailure_WhenTransactionDateIsBeforeLastTransaction()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 2000 };
            var lastTransaction = new Transaction { TransactionTime = DateTime.Now.AddDays(1) };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(1)).ReturnsAsync(lastTransaction);

            // Act
            var result = await _bankAccountService.CashWithdraw(1, 1000, "Withdraw", DateTime.Now, null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsFalse(result.Response);
            Assert.IsNotEmpty(result.Errors);
            Assert.AreEqual("CashWithdraw", result.Errors[0].PropertyName);
        }

        [Test]
        public async Task CashWithdraw_Succeeds_WithBudgetAccounts()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 2000 };
            var budgetAccounts = new List<CashWithdrawRequestBudgetAccount> { new CashWithdrawRequestBudgetAccount { Id = 1, Amount = 500 } };
            var budgetAccount = new BudgetAccount { BudgetAccountId = 1, Balance = 1000 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);
            _bankAccountRepositoryMock.Setup(x => x.GetLastTransaction(1)).ReturnsAsync((Transaction)null);
            _budgetRepositoryMock.Setup(x => x.GetLastBudgetTransaction(1)).ReturnsAsync((BudgetTransaction)null);
            _budgetRepositoryMock.Setup(x => x.GetBudgetAccountById(1)).ReturnsAsync(budgetAccount);

            // Act
            var result = await _bankAccountService.CashWithdraw(1, 1000, "Withdraw", DateTime.Now, budgetAccounts);

            // Assert
            Assert.IsTrue(result.Status);
            Assert.IsTrue(result.Response);
            Assert.IsEmpty(result.Errors);
            Assert.IsEmpty(result.ValidationErrors);
            _bankAccountRepositoryMock.Verify(x => x.CashTransfer(It.Is<Account>(a => a.Balance == 1000), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), true, It.IsAny<List<BudgetTransactionDto>>()), Times.Once());
        }
        #endregion

        #region Private Method Tests (Indirect)
        [Test]
        public async Task UpdatePrimaryAccountNumber_SetsPrimaryAccount_WhenNoAccountsExist()
        {
            // Arrange
            var model = new CreateAccountRequest
            {
                Customer = new CreateAccountRequestCustomer { Id = 1, CustomerWantCreditCard = false },
                Account = new CreateAccountRequestAccount { AccountTypeId = 1, DoYouWantToChangeThisAccountToPrimaryAccount = false },
                Bank = new CreateAccountRequestBank { Id = 1 }
            };
            var customer = new Customer { CustomerId = 1 };
            //_bankAccountRepositoryMock.Setup(x => x.GetNewAccountNumber()).ReturnsAsync("9000000000000001");
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.DebitCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.IsAnyAccountForThisCustomer(1)).ReturnsAsync(false);
            _bankAccountRepositoryMock.Setup(x => x.GetCustomerByCustomerId(1)).ReturnsAsync(customer);
            _bankAccountRepositoryMock.Setup(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.IsAny<CreditScore>(), It.IsAny<Customer>(), false, false, false)).Returns(true);

            // Act
            await _bankAccountService.CreateAccount(model);

            // Assert
            _bankAccountRepositoryMock.Verify(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.IsAny<CreditScore>(), It.Is<Customer>(c => c.PrimaryAccountNumber == "9000000000000001"), false, false, true), Times.Once());
        }

        [Test]
        public async Task GetNewAccountNumber_ReturnsIncrementedNumber()
        {
            // Arrange
            var lastAccount = new Account { AccountNumber = "9000000000000000" };
            _bankAccountRepositoryMock.Setup(x => x.GetLastAccount()).ReturnsAsync(lastAccount);
            var model = new CreateAccountRequest
            {
                Customer = new CreateAccountRequestCustomer { Id = 1, CustomerWantCreditCard = false },
                Account = new CreateAccountRequestAccount { AccountTypeId = 1 },
                Bank = new CreateAccountRequestBank { Id = 1 }
            };
            _bankAccountRepositoryMock.Setup(x => x.IsAnyAccountForThisCustomer(1)).ReturnsAsync(false);
            _bankAccountRepositoryMock.Setup(x => x.GetCustomerByCustomerId(1)).ReturnsAsync(new Customer { CustomerId = 1 });
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.DebitCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.IsAny<CreditScore>(), It.IsAny<Customer>(), false, false, false)).Returns(true);

            // Act
            await _bankAccountService.CreateAccount(model);

            // Assert
            _bankAccountRepositoryMock.Verify(x => x.CreateAccount(It.Is<Account>(a => a.AccountNumber == "9000000000000001"), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.IsAny<CreditScore>(), It.IsAny<Customer>(), false, false, false), Times.Once());
        }

        [Test]
        public async Task CalculateCreditScore_ReturnsCorrectScore()
        {
            // Arrange
            var model = new CreateAccountRequest
            {
                Customer = new CreateAccountRequestCustomer { Id = 1, CustomerWantCreditCard = false },
                Account = new CreateAccountRequestAccount { AccountTypeId = 1 },
                Bank = new CreateAccountRequestBank { Id = 1 }
            };
            //_bankAccountRepositoryMock.Setup(x => x.GetNewAccountNumber()).ReturnsAsync("9000000000000001");
            _bankAccountRepositoryMock.Setup(x => x.GetLastCard(CardType.DebitCard)).ReturnsAsync((Card)null);
            _bankAccountRepositoryMock.Setup(x => x.IsAnyAccountForThisCustomer(1)).ReturnsAsync(false);
            _bankAccountRepositoryMock.Setup(x => x.GetCustomerByCustomerId(1)).ReturnsAsync(new Customer { CustomerId = 1 });
            _bankAccountRepositoryMock.Setup(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.Is<CreditScore>(cs => cs.CreditScoreValue == 300), It.IsAny<Customer>(), false, false, false)).Returns(true);

            // Act
            await _bankAccountService.CreateAccount(model);

            // Assert
            _bankAccountRepositoryMock.Verify(x => x.CreateAccount(It.IsAny<Account>(), It.IsAny<Transaction>(), It.IsAny<TransactionDetail>(), It.IsAny<Card>(), null, It.Is<CreditScore>(cs => cs.CreditScoreValue == 300), It.IsAny<Customer>(), false, false, false), Times.Once());
        }

        [Test]
        public async Task ValidateBudgetAccount_ReturnsValidationError_WhenSumExceedsAmount()
        {
            // Arrange
            var budgetAccounts = new List<TransferAmountRequestBudgetAccount> { new TransferAmountRequestBudgetAccount { Id = 1, Amount = 1500 } };
            var account = new Account { AccountId = 1, Balance = 2000, CustomerId = 1 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);

            // Act
            var result = await _bankAccountService.TransferAmount(1, 2, 1000, "Transfer", DateTime.Now, "Tag1", budgetAccounts);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.BudgetAccount.Asmount", result.ValidationErrors[0].PropertyPath);
        }

        [Test]
        public async Task ValidateBalance_ReturnsValidationError_WhenBalanceIsInsufficient()
        {
            // Arrange
            var account = new Account { AccountId = 1, Balance = 500 };
            _bankAccountRepositoryMock.Setup(x => x.GetAccount(1)).ReturnsAsync(account);

            // Act
            var result = await _bankAccountService.CashWithdraw(1, 1000, "Withdraw", DateTime.Now, null);

            // Assert
            Assert.IsFalse(result.Status);
            Assert.IsNotEmpty(result.ValidationErrors);
            Assert.AreEqual("Request.Account.Amount", result.ValidationErrors[0].PropertyPath);
        }
        #endregion
    }
}