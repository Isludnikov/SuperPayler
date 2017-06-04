using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PaylerAPI;

namespace SuperPayler.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order_ID { get; set; }
        
        public string Card_Number { get; set; }
        public long Amount_Kop { get; set; }
        public Statuses.TransactionStatus Status { get; set; }
    }
}