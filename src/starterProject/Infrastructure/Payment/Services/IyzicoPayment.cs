using Iyzipay;
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
        public static Options GetOptions()
        {
            Options options = new Options();
            options.ApiKey = "sandbox-Lv06ifEHYcTD4k319bE7wyzcEKxuD4f3";
            options.SecretKey = "sandbox-iHJnmQt8930EOgmoD9yCoN0FhnSMDykk";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            return options;
        }
        public bool Pay(string creditCardNo, string cvc, DateTime expireTime)
        {
            return true;
        }
    }
}
