using PaylerAPI;
using System;

namespace SuperPayler.Models
{
    public class Bank:IDisposable
    {
        DBContext db = new DBContext();

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                db.Dispose();
            }
            disposed = true;
        }

        internal Statuses.RefundStatus Refund(int id)
        {
            Payment Pay = db.Payments.Find(id);
            CreditCard Cc = db.CreditCards.Find(Pay.Card_Number);
            Pay.Status = Statuses.TransactionStatus.REFUNDED;
            Cc.Amount += Pay.Amount_Kop;
            db.SaveChanges();

            return Statuses.RefundStatus.OK;
        }

        internal bool IsCreditCardExists(RawPayData RawData) => db.CreditCards.Find(RawData.Card_Number) == null ? false : true;

        internal bool IsCardValid(RawPayData RawData)
        {
            CreditCard Cc = db.CreditCards.Find(RawData.Card_Number);

            return
                RawData.Card_Number == Cc.Card_Number &&
                RawData.Expiry_Month == Cc.Expiry_Month &&
                RawData.Expiry_Year == Cc.Expiry_Year &&
                RawData.CVV == Cc.CVV /*&&
                RawData.Cardholder_Name.ToLower() == Cc.Cardholder_Name.ToLower()*/;
        }

        internal bool HasEnoughMoney(RawPayData RawData)
        {
            CreditCard Cc = db.CreditCards.Find(RawData.Card_Number);
            return !Cc.Limited || Cc.Amount >= RawData.Amount_Kop;
        }

        internal bool IsCardExpired(RawPayData RawData)
        {
            CreditCard Cc = db.CreditCards.Find(RawData.Card_Number);
            var now = DateTime.Now;
            return now.Year > Cc.Expiry_Year || (now.Year == Cc.Expiry_Year && now.Month > Cc.Expiry_Month);
        }

        internal Statuses.PaymentStatus DoPayment(Payment Pay, RawPayData RawData)
        {
            CreditCard Cc = db.CreditCards.Find(RawData.Card_Number);
            db.Payments.Add(Pay);
            Cc.Amount -= RawData.Amount_Kop;
            db.SaveChanges();
            return Statuses.PaymentStatus.OK; //code 1 OK
        }

        internal bool ExistsPayment(int id) => db.Payments.Find(id) != null;

        internal Statuses.TransactionStatus GetPaymentStatus(int id)
        {
            Payment checkedPayment = db.Payments.Find(id);
            if (checkedPayment == null)
                return Statuses.TransactionStatus.NOT_EXISTS;
            return checkedPayment.Status;
        }
    }
}