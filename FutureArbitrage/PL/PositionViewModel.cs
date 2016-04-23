using FutureArbitrage.Contract;
using FutureArbitrage.CTP;
using FutureArbitrage.Frame;
using System.Collections.Generic;

namespace FutureArbitrage.PL
{
    public class PositionViewModel : BindableBase
    {
        private PositionViewModel() { }

        private static readonly PositionViewModel instance = new PositionViewModel();
        public static PositionViewModel Instance { get { return instance; } }

        private List<InvestorPosition> positions;
        public List<InvestorPosition> Positions
        {
            get
            {
                return this.positions;
            }
            private set
            {
                this.positions = value;
                this.NotifyPropertyChanged("Positions");
            }
        }

        private List<InvestorPositionDetail> positionDetails;
        public List<InvestorPositionDetail> PositionDetails
        {
            get
            {
                return this.positionDetails;
            }
            private set
            {
                this.positionDetails = value;
                this.NotifyPropertyChanged("PositionDetails");
            }
        }

        public Command RefreshPositionCommand
        {
            get
            {
                return new Command(this.DoRefreshPosition);
            }
        }
        private void DoRefreshPosition()
        {
            this.Positions = null;
            TradeAdapter.Instance.ReqPosition();
        }

        public Command RefreshPositionDetialCommand
        {
            get
            {
                return new Command(this.DoRefreshPositionDetial);
            }
        }
        private void DoRefreshPositionDetial()
        {
            this.PositionDetails = null;
            TradeAdapter.Instance.ReqPositionDetial();
        }

        public void Update(List<InvestorPosition> positions)
        {
            this.Positions = positions;
        }

        public void Update(List<InvestorPositionDetail> positionDetails)
        {
            this.PositionDetails = positionDetails;
        }
    }
}