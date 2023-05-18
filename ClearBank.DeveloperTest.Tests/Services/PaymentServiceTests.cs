using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly MakePaymentRequest _makePaymentRequest = new MakePaymentRequest()
        {
            Amount = 1000,
            DebtorAccountNumber= "1234"
        };

        private readonly Mock<IDataStoreFactory> _dataStoreFactory = new Mock<IDataStoreFactory>();     
        private readonly Mock<IDataStore> _dataStore = new Mock<IDataStore>();

        [Fact]
        public void BacsPayment_HasFlag_ReturnsSuccess()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.Bacs;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Status = AccountStatus.Live,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);

            
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public void BacsPayment_NoFlag_ReturnsFail()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.Bacs;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                Status = AccountStatus.Live,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void FasterPayment_HasFlag_HasEnoughBalance_ReturnsSuccess()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.FasterPayments;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public void FasterPayment_HasFlag_NotEnoughBalance_ReturnsFailure()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.FasterPayments;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live,
                Balance = 500
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void FasterPayment_NoFlag_HasEnoughBalance_ReturnsFailure()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.Chaps;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                Status = AccountStatus.Live,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void FasterPayment_NoFlag_NotEnoughBalance_ReturnsFailure()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.FasterPayments;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                Status = AccountStatus.Live,
                Balance = 500
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void ChapsPayment_HasFlag_IsLive_ReturnsSuccess()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.Chaps;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public void ChapsPayment_HasFlag_NotLive_ReturnsFailure()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.FasterPayments;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void ChapsPayment_NoFlag_IsLive_ReturnsFailure()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.FasterPayments;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                Status = AccountStatus.Live,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void ChapsPayment_NoFlag_NotLive_ReturnsFailure()
        {
            _makePaymentRequest.PaymentScheme = PaymentScheme.Chaps;
            var testAccount = new Account()
            {
                AccountNumber = "1234",
                Status = AccountStatus.Disabled,
                Balance = 1000
            };
            _dataStoreFactory.Setup(x => x.CreateDataStore()).Returns(_dataStore.Object);
            _dataStore.Setup(x => x.GetAccount("1234")).Returns(testAccount);

            var paymentService = new PaymentService(_dataStoreFactory.Object);
            var result = paymentService.MakePayment(_makePaymentRequest);


            Assert.NotNull(result);
            Assert.False(result.Success);
        }
    }
}
