using PaylerAPI;
using SuperPayler.Models;
using System.Web.Http;

namespace SuperPayler.Controllers
{
    public class GetStatusController : ApiController
    {
        // GET api/GetStatus/id

        [HttpGet]
        public Statuses.TransactionStatus GetStatus(int id)
        {
            using (Bank bnk = new Bank())
            {
                return bnk.GetPaymentStatus(id);
            }
        }
    }
}
