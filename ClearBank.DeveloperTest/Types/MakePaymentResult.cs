﻿namespace ClearBank.DeveloperTest.Types
{
    public class MakePaymentResult
    {
        public bool Success { get; set; }

        public MakePaymentResult(bool success) { Success = success; }
    }
}
