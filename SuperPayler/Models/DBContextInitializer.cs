using PaylerAPI;
using System.Data.Entity;

namespace SuperPayler.Models
{
    public class DBContextInitializer:DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            context.CreditCards.Add(new CreditCard { Card_Number = "1234567890123456", Expiry_Month = 5, Expiry_Year = 2017, CVV = 111, Cardholder_Name = "Ivan Ivanov", Amount = 10000, Limited = true });
            context.CreditCards.Add(new CreditCard { Card_Number = "4444555566667777", Expiry_Month = 10, Expiry_Year = 2018, CVV = 444, Cardholder_Name = "Petr Petrov", Amount = 35000, Limited = true });
            context.CreditCards.Add(new CreditCard { Card_Number = "9999999999999999", Expiry_Month = 12, Expiry_Year = 2020, CVV = 999, Cardholder_Name = "Rich Man", Amount = 0, Limited = false });

            base.Seed(context);
        }
    }
}