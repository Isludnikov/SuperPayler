namespace PaylerAPI
{
    public class RawPayData
    {
        public int Order_ID { get; set; }
        public string Card_Number { get; set; }
        public byte Expiry_Month { get; set; }
        public short Expiry_Year { get; set; }
        public short CVV { get; set; }
        public string Cardholder_Name { get; set; }
        public long Amount_Kop { get; set; }
    }
}