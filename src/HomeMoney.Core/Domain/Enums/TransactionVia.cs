using System;

namespace HomeMoney.Core.Domain.Enums
{
    [Flags]
    public enum TransactionVia 
    {
        None = 0,
        Cash = 1 << 0, //1
        Bancomat = 1 << 1, //2
        CreditCard = 1 << 2, //4
        BankTransfer = 1 << 3, //8
        PostalOrder = 1 << 4, //16
        Rid = 1 << 5 // 32
    }
}