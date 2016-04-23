using FutureArbitrage.Contract;
using FutureArbitrage.CTP;
using FutureArbitrage.Frame;

namespace FutureArbitrage.PL
{
    public class InstrumentModel : BindableBase
    {
        public InstrumentModel()
        {
            this.OffsetFlag = OffsetFlag.Open;
            this.Volume = 1;
        }

        private string instrumentID;
        public string InstrumentID
        {
            get
            {
                return this.instrumentID;
            }
            set
            {
                this.Price = 0;
                string preInstrumentID = this.instrumentID;
                this.instrumentID = value;
                if (preInstrumentID != null)
                {
                    ArbitrageViewModel.Instance.TryUnSubscribe(preInstrumentID);
                }
                MdAdapter.Subscribe(value);
            }
        }

        private OffsetFlag offsetFlag;
        public OffsetFlag OffsetFlag
        {
            get
            {
                return this.offsetFlag;
            }
            set
            {
                this.offsetFlag = value;
                this.NotifyPropertyChanged("OffsetFlag");
            }
        }

        private DirectionType direction;
        public DirectionType Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
                this.NotifyPropertyChanged("Direction");
                this.Price = 0;
            }
        }

        private int volume;
        public int Volume
        {
            get
            {
                return this.volume;
            }
            set
            {
                this.volume = value;
                this.NotifyPropertyChanged("Volume");
            }
        }

        private double price;
        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
                this.NotifyPropertyChanged("Price");
            }
        }
    }
}