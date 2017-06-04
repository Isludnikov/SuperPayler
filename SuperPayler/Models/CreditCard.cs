using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperPayler.Models
{
    public class CreditCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Card_Number { get; set; }
        public byte Expiry_Month { get; set; }
        public short Expiry_Year { get; set; }
        public short CVV { get; set; }
        public string Cardholder_Name { get; set; }
        public bool Limited { get; set; }
        public long Amount { get; set; }
    }
}