using Infrastructure.Payment.Services;
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
    bool Pay(string creditCartNo, short cvc, DateTime expireTime);
    bool VerifyPayment(string conversationId);
}

