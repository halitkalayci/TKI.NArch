using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payment.Services.Models;
public class PaymentModel
{
    public string CardHolderName { get; set; }
    public string CardNo { get; set; }
    public short CVC { get; set; }
    public DateTime ExpireTime { get; set; }
}
