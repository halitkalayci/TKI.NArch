using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCKNService;

namespace Infrastructure.Verification.TCKN;
public class TCKNVerificationService : IVerificationService
{
    public async Task<bool> VerifyTCKN(long tckn, string ad, string soyad, int dogumYili)
    {
        KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12);

        var response = await client.TCKimlikNoDogrulaAsync(tckn, ad, soyad, dogumYili);

        return response.Body.TCKimlikNoDogrulaResult;
    }
}
