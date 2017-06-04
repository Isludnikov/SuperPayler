using PaylerAPI;
using System.Data.Entity;

namespace SuperPayler.Models
{
    public class DBContext:DbContext
    {
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}