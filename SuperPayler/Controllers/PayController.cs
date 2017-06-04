using PaylerAPI;
using SuperPayler.Models;
using System;
using System.Web.Http;

namespace SuperPayler.Controllers
{
    public class PayController : ApiController
    {
        // POST api/Pay/5
        [HttpPost]
        public Statuses.PaymentStatus Pay(RawPayData RawData)
        {
            using (Bank bnk = new Bank())
            {

                if (bnk.ExistsPayment(RawData.Order_ID)) return Statuses.PaymentStatus.PAY_ALREADY_EXISTS;

                if (!bnk.IsCreditCardExists(RawData))
                    return Statuses.PaymentStatus.CARD_REQ_INCORRECT;

                if (!bnk.IsCardValid(RawData)) return Statuses.PaymentStatus.CARD_REQ_INCORRECT;

                if (bnk.IsCardExpired(RawData)) return Statuses.PaymentStatus.CARD_EXPIRED;

                if (!bnk.HasEnoughMoney(RawData)) return Statuses.PaymentStatus.INSUFFICIENT_FUNDS;

                // emulate bank error
                /*Random rnd = new Random();
                if (rnd.Next(20) <= 1)
                    return Statuses.PaymentStatus.BANK_PROCESSING_ERROR;*/

                try
                {
                    return bnk.DoPayment(new Payment { Order_ID = RawData.Order_ID, Amount_Kop = RawData.Amount_Kop, Card_Number = RawData.Card_Number, Status = Statuses.TransactionStatus.OK }, RawData);
                }
                catch
                {
                    return Statuses.PaymentStatus.PAYGATE_ERROR;
                }
            }
        }
    }
}
