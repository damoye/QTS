using CTP;

namespace Host.Contract
{
    public class Instrument
    {
        public string InstrumentID { get; set; }
        public string ExchangeID { get; set; }
        public string InstrumentName { get; set; }
        public string ExchangeInstID { get; set; }
        public string ProductID { get; set; }
        public EnumProductClassType ProductClass { get; set; }
        public int DeliveryYear { get; set; }
        public int DeliveryMonth { get; set; }
        public int MaxMarketOrderVolume { get; set; }
        public int MinMarketOrderVolume { get; set; }
        public int MaxLimitOrderVolume { get; set; }
        public int MinLimitOrderVolume { get; set; }
        public int VolumeMultiple { get; set; }
        public double PriceTick { get; set; }
        public string CreateDate { get; set; }
        public string OpenDate { get; set; }
        public string ExpireDate { get; set; }
        public string StartDelivDate { get; set; }
        public string EndDelivDate { get; set; }
        public EnumInstLifePhaseType InstLifePhase { get; set; }
        public int IsTrading { get; set; }
        public EnumPositionTypeType PositionType { get; set; }
        public EnumPositionDateTypeType PositionDateType { get; set; }
        public double LongMarginRatio { get; set; }
        public double ShortMarginRatio { get; set; }
        public EnumMaxMarginSideAlgorithmType MaxMarginSideAlgorithm { get; set; }
        public string UnderlyingInstrID { get; set; }
        public double StrikePrice { get; set; }
        public EnumOptionsTypeType OptionsType { get; set; }
        public double UnderlyingMultiple { get; set; }
        public EnumCombinationTypeType CombinationType { get; set; }

        public Instrument() { }

        public Instrument(ThostFtdcInstrumentField field)
        {
            this.InstrumentID = field.InstrumentID;
            this.ExchangeID = field.ExchangeID;
            this.InstrumentName = field.InstrumentName;
            this.ExchangeInstID = field.ExchangeInstID;
            this.ProductID = field.ProductID;
            this.ProductClass = field.ProductClass;
            this.DeliveryYear = field.DeliveryYear;
            this.DeliveryMonth = field.DeliveryMonth;
            this.MaxMarketOrderVolume = field.MaxMarketOrderVolume;
            this.MinMarketOrderVolume = field.MinMarketOrderVolume;
            this.MaxLimitOrderVolume = field.MaxLimitOrderVolume;
            this.MinLimitOrderVolume = field.MinLimitOrderVolume;
            this.VolumeMultiple = field.VolumeMultiple;
            this.PriceTick = field.PriceTick;
            this.CreateDate = field.CreateDate;
            this.OpenDate = field.OpenDate;
            this.ExpireDate = field.ExpireDate;
            this.StartDelivDate = field.StartDelivDate;
            this.EndDelivDate = field.EndDelivDate;
            this.InstLifePhase = field.InstLifePhase;
            this.IsTrading = field.IsTrading;
            this.PositionType = field.PositionType;
            this.PositionDateType = field.PositionDateType;
            this.LongMarginRatio = field.LongMarginRatio;
            this.ShortMarginRatio = field.ShortMarginRatio;
            this.MaxMarginSideAlgorithm = field.MaxMarginSideAlgorithm;
            this.UnderlyingInstrID = field.UnderlyingInstrID;
            this.StrikePrice = field.StrikePrice;
            this.OptionsType = field.OptionsType;
            this.UnderlyingMultiple = field.UnderlyingMultiple;
            this.CombinationType = field.CombinationType;
        }

        public override string ToString()
        {
            string result = string.Format("{0}\t\t\t{1}\t\t\t{2}", this.InstrumentID, this.InstrumentName, this.ExchangeID);
            return result;
        }
    }
}
