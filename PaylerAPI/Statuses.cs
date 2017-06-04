namespace PaylerAPI
{
    public class Statuses
    {
        public enum PaymentStatus
        {
            OK = 1,
            BANK_PROCESSING_ERROR,
            CARD_REQ_INCORRECT,
            CARD_EXPIRED,
            PAY_ALREADY_EXISTS,
            INSUFFICIENT_FUNDS,
            PAYGATE_ERROR
        }
        public enum RefundStatus
        {
            OK = 1,
            HAS_ERROR,
            ALREADY_REFUNDED,
            NOT_EXISTS,
            ERROR
        }
        public enum TransactionStatus
        {
            OK = 1,
            ERROR,
            REFUNDED,
            NOT_EXISTS
        }
    }
}
