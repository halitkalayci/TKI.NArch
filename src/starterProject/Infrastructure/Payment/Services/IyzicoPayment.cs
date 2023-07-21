using Infrastructure.Payment.Adapters;
using Infrastructure.Payment.Services.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Model.V2.Transaction;
using Iyzipay.Request;
using Iyzipay.Request.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payment.Services
{
    // Dış kütüphane, IyziCo developerları tarafından yazılmış.
    public class IyzicoPayment : IPosServiceAdapter
    {
        public static Options GetOptions()
        {
            Options options = new Options();
            options.ApiKey = "sandbox-Lv06ifEHYcTD4k319bE7wyzcEKxuD4f3";
            options.SecretKey = "sandbox-iHJnmQt8930EOgmoD9yCoN0FhnSMDykk";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            return options;
        }
        public PaymentResponseModel Pay(string creditCartNo, short cvc, DateTime expireTime)
        {
            #region directPay
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Price = "50";
            request.PaidPrice = "60";
            request.Locale = Locale.TR.ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = Guid.NewGuid().ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = creditCartNo;
            paymentCard.Cvc = cvc.ToString();
            paymentCard.ExpireMonth = expireTime.Month.ToString();
            paymentCard.ExpireYear = expireTime.Year.ToString();

            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Ip = "127.0.1.1";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.Email = "johndoe@gmail.com";
            buyer.GsmNumber = "1";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            buyer.Id = "1";

            request.Buyer = buyer;


            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BMW01";
            firstBasketItem.Name = "BMW Kiralama Hizmeti";
            firstBasketItem.Category1 = "Hizmet";
            firstBasketItem.Category2 = "Araç Kiralama";
            firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            firstBasketItem.Price = "50";
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;

            Iyzipay.Model.Payment payment = Iyzipay.Model.Payment.Create(request, GetOptions());
            return new PaymentResponseModel()
            {
                Success = payment.Status == Status.SUCCESS.ToString(),
                ErrorMessage = payment.ErrorMessage
            };
            #endregion
            #region 3D
           
            #endregion
        }

        public Payment3DResponseModel PayWith3D(string creditCartNo, short cvc, DateTime expireTime)
        {
            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Price = "50";
            request.PaidPrice = "60";
            request.Locale = Locale.TR.ToString();
            request.Currency = Currency.TRY.ToString();
            request.BasketId = Guid.NewGuid().ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "http://localhost:5210/api/payments/checkout-completed";


            Buyer buyer = new Buyer();
            buyer.Ip = "127.0.1.1";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.Email = "johndoe@gmail.com";
            buyer.GsmNumber = "1";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            buyer.Id = "1";

            request.Buyer = buyer;


            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BMW01";
            firstBasketItem.Name = "BMW Kiralama Hizmeti";
            firstBasketItem.Category1 = "Hizmet";
            firstBasketItem.Category2 = "Araç Kiralama";
            firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            firstBasketItem.Price = "50";
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;

            CheckoutFormInitialize payment = CheckoutFormInitialize.Create(request, GetOptions());
            return new Payment3DResponseModel()
            {
                Link = payment.PaymentPageUrl,
                Html=payment.CheckoutFormContent
            };
        }

        // Kiralama => ConversationId db => Kiralama için ödeme tamamlandı mı?
        // True => kiralama başarılı
        // False => kiralama başarısız
        public bool VerifyPayment(string conversationId)
        {
            RetrieveTransactionDetailRequest request = new RetrieveTransactionDetailRequest()
            {
                PaymentConversationId = conversationId
            };
            TransactionDetail transactionDetail = TransactionDetail.Retrieve(request, GetOptions());

            return transactionDetail.Status == Status.SUCCESS.ToString();
        }
    }
}
