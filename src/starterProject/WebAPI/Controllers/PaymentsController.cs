using Infrastructure.Payment.Adapters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentsController : BaseController
{
    private IPosServiceAdapter _posServiceAdapter;

    [HttpPost]
    public IActionResult Pay([FromBody] PaymentModel model)
    {
        _posServiceAdapter = new IyziCoPosServiceAdapter();
        return Ok(_posServiceAdapter.Pay(model.CreditCardNo, model.CVC, model.ExpireTime));
    }
    [HttpPost("checkout-completed")]
    public IActionResult CheckoutCompleted()
    {
        return Ok("Ödeme tamamlandı.");
    }
}

public class PaymentModel
{
    public string CreditCardNo { get; set; }
    public short CVC { get; set; }
    public DateTime ExpireTime { get; set; }
}
