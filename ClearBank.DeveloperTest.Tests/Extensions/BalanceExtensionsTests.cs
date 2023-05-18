using ClearBank.DeveloperTest.Extensions;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Extensions
{
    public class BalanceExtensionsTests
    {

        private readonly MakePaymentRequest _makePaymentRequest = new MakePaymentRequest() { };

        [Theory]
        [InlineData(50, 500)]
        [InlineData(5, 500)]
        [InlineData(1, 50000)]
        [InlineData(500, 500)]
        public void DeductAmountFromBalance(decimal amount, decimal balance)
        {
            _makePaymentRequest.Amount = amount;

            var testAccount = new Account()
            {
                Balance = balance
            };

            var result = testAccount.Balance - _makePaymentRequest.Amount;
            BalanceExtensions.DeductAmountFromBalance(_makePaymentRequest, testAccount);

            Assert.Equal(testAccount.Balance, result);

        }
    }
}
