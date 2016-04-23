using CTP;
using Host.Common;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Host.BLL.CTP
{
    public class MdAdapter : CTPMdAdapter
    {
        private string[] instrumentIDs;
        private QueueThreadSafe<ThostFtdcDepthMarketDataField> marketDataFieldQueue = new QueueThreadSafe<ThostFtdcDepthMarketDataField>();
        private DateTime lastQuotationTime;
        private bool isConnected;

        private static MdAdapter instance = new MdAdapter();
        public static MdAdapter Instance { get { return instance; } }
        private MdAdapter() { }

        public void Start(string[] instrumentIDs)
        {
            this.instrumentIDs = instrumentIDs;
            this.RegisterFront("tcp://180.168.146.187:10010");
            this.Init();
            var thread = new Thread(this.PublishQuotation);
            thread.IsBackground = true;
            thread.Name = "MdAdapter";
            thread.Start();
        }

        public override void OnFrontConnected()
        {
            this.isConnected = true;
            var loginField = new ThostFtdcReqUserLoginField { BrokerID = "9999", UserID = "014749", Password = "TODO" };
            this.ReqUserLogin(loginField, 0);
        }

        public override void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            this.SubscribeMarketData(this.instrumentIDs);
        }

        public override void OnRtnDepthMarketData(ThostFtdcDepthMarketDataField pDepthMarketData)
        {
            this.marketDataFieldQueue.Enqueue(pDepthMarketData);
        }

        public void PublishQuotation()
        {
            while (true)
            {
                if (marketDataFieldQueue.Count == 0)
                {
                    Thread.Sleep(1);
                    continue;
                }
                this.lastQuotationTime = DateTime.Now;
                ThostFtdcDepthMarketDataField field = marketDataFieldQueue.Dequeue();
                if (field.LastPrice == 0)
                {
                    Debug.WriteLine("Filter data with no price: " + field.InstrumentID);
                    continue;
                }
                field.TradingDay = this.GetTradingDay();
                if (field.ActionDay == string.Empty)
                {
                    var today = DateTime.Today;
                    var builder = new StringBuilder(today.Year.ToString());
                    if (today.Month < 10)
                    {
                        builder.Append(0);
                    }
                    builder.Append(today.Month);
                    if (today.Day < 10)
                    {
                        builder.Append(0);
                    }
                    builder.Append(today.Day);
                    field.ActionDay = builder.ToString();
                }
                QuotationCenter.ProcessMarketDataField(field);
            }
        }

        public string GetStatus()
        {
            return "CTPMdAdapter's last time that received a quotation: " + this.lastQuotationTime +
                   Environment.NewLine +
                   "CTPMdAdapter quotation queue's cout: " + this.marketDataFieldQueue.Count +
                   Environment.NewLine +
                   "CTPMdAdapter is Connected: " + this.isConnected;
        }

        #region useless methods
        public override void OnFrontDisconnected(int nReason)
        {
            this.isConnected = false;
        }
        public override void OnRspSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast) { }
        public override void OnHeartBeatWarning(int nTimeLapse)
        {
            throw new NotImplementedException();
        }
        public override void OnRspError(ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspUserLogout(ThostFtdcUserLogoutField pUserLogout, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspUnSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspSubForQuoteRsp(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRspUnSubForQuoteRsp(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }
        public override void OnRtnForQuoteRsp(ThostFtdcForQuoteRspField pForQuoteRsp)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
