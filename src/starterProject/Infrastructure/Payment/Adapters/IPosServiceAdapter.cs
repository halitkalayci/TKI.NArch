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
}


public class IyziCoPosServiceAdapter : IPosServiceAdapter
{
    public bool Pay(string creditCartNo, short cvc, DateTime expireTime)
    {
        // Sıkı bağımlılığın çözüleceği nokta
        // Adaptasyon kısmı
        IyzicoPayment iyzicoPayment = new();
        return iyzicoPayment.Pay(creditCartNo, cvc.ToString(), expireTime);
    }
}

public class StripePosServiceAdapter : IPosServiceAdapter
{
    public bool Pay(string creditCartNo, short cvc, DateTime expireTime)
    {
        StripePayment payment = new();
        string expireTimeAsString = expireTime.Month + "/" + expireTime.Year;
        return payment.MakePayment(creditCartNo, cvc.ToString(), expireTimeAsString);
    }
}
