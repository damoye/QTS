using FutureArbitrage.Converter;

namespace FutureArbitrage.Contract
{
    public class InputOrder
    {
        private static OffsetFlagConverter offsetFlagConverter = new OffsetFlagConverter();
        private static DirectionConverter directionConverter = new DirectionConverter();

        public InputOrder(
            string brokerID,
            string investorID,
            DirectionType direction,
            OffsetFlag offsetFlag,
            string instrumentID,
            double price,
            int volume,
            string groupID)
        {
            this.BrokerID = brokerID;
            this.InvestorID = investorID;
            this.Direction = direction;
            this.OffsetFlag = offsetFlag;
            this.InstrumentID = instrumentID;
            this.Price = price;
            this.Volume = volume;
            this.GroupID = groupID;
        }

        public string BrokerID { get; set; }
        public string InvestorID { get; set; }
        public DirectionType Direction { get; set; }
        public OffsetFlag OffsetFlag { get; set; }
        public string InstrumentID { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public string GroupID { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} 量：{4} 价：{3}", this.InstrumentID, offsetFlagConverter.Convert(this.OffsetFlag), directionConverter.Convert(this.Direction), this.Volume, this.Price);
        }
    }
}