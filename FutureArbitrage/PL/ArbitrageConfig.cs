using FutureArbitrage.Contract;
namespace FutureArbitrage.PL
{
    public class ArbitrageConfig
    {
        public InstrumentModel[] InstrumentConfigs { get; set; }
        public RelativeSymbol Symbol { get; set; }
        public double TriggerPrice { get; set; }
        public int Multiple { get; set; }
    }
}