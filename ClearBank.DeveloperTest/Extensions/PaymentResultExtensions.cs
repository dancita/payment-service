using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Extensions
{
    public class PaymentResultExtensions
    {
        public static void SetSuccessByPaymentSchemesFlag(Account account, MakePaymentResult result, AllowedPaymentSchemes allowedPaymentSchemeType)
        {
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(allowedPaymentSchemeType))
            {
                result.Success = false;
            }
        }
    }
}
