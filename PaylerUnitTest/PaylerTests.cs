using Xunit;
using PaylerAPI;
using System;
using System.Data.Entity;
using SuperPayler.Models;
using SuperPayler.Controllers;
using System.Web.Http;

namespace PaylerUnitTest
{
    public class PaylerTests
    {
        [Fact]
        public void CorrectPayment()
        {
            PrepareDB();
            Assert.Equal(Statuses.PaymentStatus.OK, new PayController().Pay(new RawPayData { Amount_Kop = 100, Cardholder_Name="Petr Petrov", Card_Number = "4444555566667777", CVV = 444, Expiry_Month = 10, Expiry_Year=2018, Order_ID=1}));
        }

        [Fact]
        public void Overpay()
        {
            PrepareDB();
            Assert.Equal(Statuses.PaymentStatus.INSUFFICIENT_FUNDS, new PayController().Pay(new RawPayData { Amount_Kop = 100000, Cardholder_Name = "Petr Petrov", Card_Number = "4444555566667777", CVV = 444, Expiry_Month = 10, Expiry_Year = 2018, Order_ID = 2 }));
        }

        [Fact]
        public void OverpayNoLimit()
        {
            PrepareDB();
            Assert.Equal(Statuses.PaymentStatus.OK, new PayController().Pay(new RawPayData { Amount_Kop = 100000, Cardholder_Name = "Rich Bitch", Card_Number = "9999999999999999", CVV = 999, Expiry_Month = 12, Expiry_Year = 2020, Order_ID = 3 }));
        }

        [Fact]
        public void OverpayNoLimitBadCVV()
        {
            PrepareDB();
            Assert.Equal(Statuses.PaymentStatus.CARD_REQ_INCORRECT, new PayController().Pay(new RawPayData { Amount_Kop = 100000, Cardholder_Name = "Satan", Card_Number = "9999999999999999", CVV = 666, Expiry_Month = 12, Expiry_Year = 2020, Order_ID = 4 }));
        }

        [Fact]
        public void ComplexTestRefund()
        {
            PrepareDB();
            Assert.Equal(Statuses.PaymentStatus.OK, new PayController().Pay(new RawPayData { Amount_Kop = 100000, Cardholder_Name = "Rich Bitch", Card_Number = "9999999999999999", CVV = 999, Expiry_Month = 12, Expiry_Year = 2020, Order_ID = 5 }));
            Assert.Equal(Statuses.TransactionStatus.OK, new GetStatusController().GetStatus(5));
            Assert.Equal(Statuses.RefundStatus.OK, new RefundController().Refund(5));
            Assert.Equal(Statuses.RefundStatus.ALREADY_REFUNDED, new RefundController().Refund(5));
        }

        private void PrepareDB()
        {
            Database.SetInitializer(new DBContextInitializer());
        }
    }
}
