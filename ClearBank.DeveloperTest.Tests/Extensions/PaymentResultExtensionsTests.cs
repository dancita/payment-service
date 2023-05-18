using ClearBank.DeveloperTest.Extensions;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Extensions
{
    public class PaymentResultExtensionsTests
    {
        private Account _account = new Account();
        private MakePaymentResult _makePaymentResult = new MakePaymentResult(true);
        private AllowedPaymentSchemes _allowedPaymentSchemes = new AllowedPaymentSchemes();

        [Fact]
        public void NullAccount_SuccessShouldBeFalse()
        {
            _account = null;
            PaymentResultExtensions.SetSuccessByPaymentSchemesFlag(_account, _makePaymentResult, _allowedPaymentSchemes);

            Assert.False(_makePaymentResult.Success);
        }

        [Fact]
        public void Account_HasRelevantFlag_SuccessShouldBeTrue()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            PaymentResultExtensions.SetSuccessByPaymentSchemesFlag(_account, _makePaymentResult, AllowedPaymentSchemes.FasterPayments);

            Assert.True(_makePaymentResult.Success);
        }

        [Fact]
        public void AccountNotNull_HasNoRelevantFlag_SuccessShouldBeFalse()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            PaymentResultExtensions.SetSuccessByPaymentSchemesFlag(_account, _makePaymentResult, AllowedPaymentSchemes.Bacs);

            Assert.False(_makePaymentResult.Success);
        }
    }
}
