using Infrastructure.Payment.Services;
using Infrastructure.Payment.Services.Models;
using Iyzipay.Model.V2.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payment.Adapters;
public interface IPosServiceAdapter
{
    // 12/27, 12/2027
    // string, datetime, (int month, int year) 
    PaymentResponseModel Pay(string creditCartNo, short cvc, DateTime expireTime);
    Payment3DResponseModel PayWith3D(string creditCartNo, short cvc, DateTime expireTime);
    TransactionDetail VerifyPayment(string conversationId);
}

