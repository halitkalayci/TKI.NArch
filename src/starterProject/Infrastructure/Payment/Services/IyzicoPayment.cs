using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payment.Services
{
    // Dış kütüphane, IyziCo developerları tarafından yazılmış.
    public class IyzicoPayment
    {
        public bool Pay(string creditCardNo, string cvc, DateTime expireTime)
        {
            return true;
        }
    }
}
