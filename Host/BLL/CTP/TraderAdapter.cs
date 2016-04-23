using CTP;
using Host.Contract;
using Host.DAL;
using Host.PL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Host.BLL.CTP
{
    public class TradeAdapter : CTPTraderAdapter
    {
        private List<Instrument> instruments = new List<Instrument>();
        private bool isConnected;

        private static TradeAdapter instance = new TradeAdapter();
        public static TradeAdapter Instance { get { return instance; } }
        private TradeAdapter() { }

        public void Start()
        {
            this.RegisterFront("tcp://180.168.146.187:10000");
            this.Init();
        }

        public override void OnFrontConnected()
        {
            this.isConnected = true;
            this.ReqUserLogin(new ThostFtdcReqUserLoginField { BrokerID = "9999", UserID = "014749", Password = "TODO" }, 0);
        }

        public override void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            this.instruments.Clear();
            this.ReqQryInstrument(new ThostFtdcQryInstrumentField(), 0);
        }

        public override void OnRspQryInstrument(ThostFtdcInstrumentField pInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            this.instruments.Add(new Instrument(pInstrument));
            if (bIsLast)
            {
                string[] instrumentIDs = this.instruments.Select(p => p.InstrumentID).ToArray();
                MdAdapter.Instance.Start(instrumentIDs);
                Instrument[] instruments = this.instruments.OrderBy(p => p.InstrumentID).ToArray();
                InstrumentListViewModel.Instance.Instruments = instruments;
                InstrumentDAL.Save(instruments);
            }
        }

        public string GetStatus()
        {
            return "TradeAdapter is Connected: " + this.isConnected;
        }

        #region useless methods
        public override void OnFrontDisconnected(int nReason)
        {
            this.isConnected = false;
        }
        public override void OnRspUserLogout(ThostFtdcUserLogoutField pUserLogout, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspOrderInsert(ThostFtdcInputOrderField pInputOrder, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspQryInvestorPosition(ThostFtdcInvestorPositionField pInvestorPosition, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspQryTradingAccount(ThostFtdcTradingAccountField pTradingAccount, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspQryInvestorPositionDetail(ThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspError(ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRtnOrder(ThostFtdcOrderField pOrder)
        {
            throw new NotImplementedException();
        }
        public override void OnRtnTrade(ThostFtdcTradeField pTrade)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
