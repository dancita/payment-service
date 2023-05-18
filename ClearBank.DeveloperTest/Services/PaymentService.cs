using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Extensions;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        IDataStoreFactory _dataStoreFactory;

        public PaymentService(IDataStoreFactory dataStoreFactory) {
            _dataStoreFactory = dataStoreFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            IDataStore accountDataStore = _dataStoreFactory.CreateDataStore();

            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult(true);
            
            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    PaymentResultExtensions.SetSuccessByPaymentSchemesFlag(account, result, AllowedPaymentSchemes.Bacs);
                    break;

                case PaymentScheme.FasterPayments:
                    PaymentResultExtensions.SetSuccessByPaymentSchemesFlag(account, result, AllowedPaymentSchemes.FasterPayments);
                    if (result.Success && account.Balance < request.Amount)
                    {
                        result.Success = false;
                    }
                    break;

                case PaymentScheme.Chaps:
                    PaymentResultExtensions.SetSuccessByPaymentSchemesFlag(account, result, AllowedPaymentSchemes.Chaps);
                    if (result.Success && account.Status != AccountStatus.Live)
                    {
                        result.Success = false;
                    }
                    break;
            }

            if (result.Success)
            {
                BalanceExtensions.DeductAmountFromBalance(request, account);

                accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
