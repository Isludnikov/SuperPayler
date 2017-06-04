using PaylerAPI;
using SuperPayler.Models;
using System.Web.Http;

namespace SuperPayler.Controllers
{
    public class RefundController : ApiController
    {
        // GET api/Refund/id
        [HttpGet]
        public Statuses.RefundStatus Refund(int id)
        {
            using (Bank bnk = new Bank())
            {
                Statuses.TransactionStatus Status = bnk.GetPaymentStatus(id);
                switch (Status)
                {
                    case Statuses.TransactionStatus.REFUNDED:
                        return Statuses.RefundStatus.ALREADY_REFUNDED;
                    case Statuses.TransactionStatus.NOT_EXISTS:
                        return Statuses.RefundStatus.NOT_EXISTS;
                    case Statuses.TransactionStatus.ERROR:
                        return Statuses.RefundStatus.HAS_ERROR;
                    case Statuses.TransactionStatus.OK:
                        return bnk.Refund(id);
                    default:
                        return Statuses.RefundStatus.ERROR;
                }
            }
        }
    }
}
