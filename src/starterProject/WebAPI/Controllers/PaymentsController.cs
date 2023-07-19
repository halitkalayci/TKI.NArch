using Infrastructure.Payment.Adapters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentsController : BaseController
{
    private IPosServiceAdapter _posServiceAdapter;

    public PaymentsController(IPosServiceAdapter posServiceAdapter)
    {
        _posServiceAdapter = posServiceAdapter;
    }

    [HttpPost]
    public IActionResult Pay([FromBody] PaymentModel model)
    {
        return Ok(_posServiceAdapter.Pay(model.CreditCardNo, model.CVC, model.ExpireTime));
    }
    [HttpPost("checkout-completed")]
    public IActionResult CheckoutCompleted()
    {
        _posServiceAdapter.VerifyPayment("123");
        return Ok("Ödeme tamamlandı.");
    }
}

public class PaymentModel
{
    public string CreditCardNo { get; set; }
    public short CVC { get; set; }
    public DateTime ExpireTime { get; set; }
}
