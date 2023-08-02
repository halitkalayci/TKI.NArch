using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Verification.TCKN;
public interface IVerificationService
{
    Task<bool> VerifyTCKN(long tckn, string ad, string soyad, int dogumYili);
}
