namespace FutureArbitrage.Contract
{
    public enum OrderStatus
    {
        AllTraded = 48,
        PartTradedQueueing = 49,
        PartTradedNotQueueing = 50,
        NoTradeQueueing = 51,
        NoTradeNotQueueing = 52,
        Canceled = 53,
        Unknown = 97,
        NotTouched = 98,
        Touched = 99
    }
}