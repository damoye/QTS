using CTP;

namespace FutureArbitrage.Contract
{
    public class Instrument
    {
        public Instrument(ThostFtdcInstrumentField field)
        {
            this.InstrumentID = field.InstrumentID;
        }

        public string InstrumentID { get; set; }
    }
}
