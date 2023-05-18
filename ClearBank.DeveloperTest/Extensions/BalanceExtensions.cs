using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Extensions
{
    public static class BalanceExtensions
    {
        public static void DeductAmountFromBalance(MakePaymentRequest request, Account account)
        {
            account.Balance -= request.Amount;
        }
    }
}
