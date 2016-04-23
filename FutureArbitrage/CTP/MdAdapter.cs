using CTP;
using FutureArbitrage.BLL;
using FutureArbitrage.Contract;
using FutureArbitrage.PL;
using FutureArbitrage.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FutureArbitrage.CTP
{
    public class MdAdapter : CTPMdAdapter
    {
        private static HashSet<string> subscribeSet = new HashSet<string>();
        private static QueueThreadSafe<ThostFtdcDepthMarketDataField> marketDataFieldQueue = new QueueThreadSafe<ThostFtdcDepthMarketDataField>();

        private static MdAdapter instance;
        public static MdAdapter Instance { get { return instance; } }

        static MdAdapter()
        {
            var thread = new Thread(PublishQuotation);
            thread.IsBackground = true;
            thread.Name = "MdAdapter";
            thread.Start();
        }

        private MdAdapter() { }

        public static void Subscribe(string instrumentID)
        {
            subscribeSet.Add(instrumentID);
            if (Instance != null && AccountCenter.Instance.IsMDLogin)
            {
                int i = Instance.SubscribeMarketData(new string[] { instrumentID });
                if (i != 0)
                {
                    LogCenter.Error("订阅行情错误：" + CTPErrorHelper.GetError(i));
                }
            }
        }

        public static void UnSubscribe(string instrumentID)
        {
            subscribeSet.Remove(instrumentID);
            if (Instance != null && AccountCenter.Instance.IsMDLogin)
            {
                int i = Instance.UnSubscribeMarketData(new string[] { instrumentID });
                if (i != 0)
                {
                    LogCenter.Error("退订行情错误：" + CTPErrorHelper.GetError(i));
                }
            }
        }

        public static void ChangeAccount()
        {
            AccountCenter.Instance.IsMDLogin = false;
            if (instance != null)
            {
                instance.Release();
            }
            instance = new MdAdapter();
            instance.RegisterFront("tcp://" + AccountCenter.Instance.Account.MDAddress);
            instance.Init();
        }

        public override void OnFrontConnected()
        {
            LogCenter.Log("行情连接成功");
            var loginField = new ThostFtdcReqUserLoginField
            {
                BrokerID = AccountCenter.Instance.Account.BrokerID,
                UserID = AccountCenter.Instance.Account.InvestorID,
                Password = AccountCenter.Instance.Account.Password
            };
            int i = this.ReqUserLogin(loginField, 0);
            if (i != 0)
            {
                LogCenter.Error("行情登录失败：" + CTPErrorHelper.GetError(i));
            }
        }

        public override void OnFrontDisconnected(int nReason)
        {
            LogCenter.Error("行情断开：{0} {1}", nReason, CTPErrorHelper.GetFrontDisconnectedReason(nReason));
            AccountCenter.Instance.IsMDLogin = false;
        }

        public override void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("行情登录失败：" + pRspInfo.ErrorMsg);
                return;
            }
            LogCenter.Log("行情登录成功");
            AccountCenter.Instance.IsMDLogin = true;
            int i = this.SubscribeMarketData(subscribeSet.ToArray());
            if (i != 0)
            {
                LogCenter.Error("订阅行情错误：" + CTPErrorHelper.GetError(i));
            }
        }

        public override void OnRspUserLogout(ThostFtdcUserLogoutField pUserLogout, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }

        public override void OnRspSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("订阅行情错误：" + pRspInfo.ErrorMsg);
            }
        }

        public override void OnRspUnSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("取消订阅行情错误：" + pRspInfo.ErrorMsg);
            }
        }

        public override void OnRtnDepthMarketData(ThostFtdcDepthMarketDataField pDepthMarketData)
        {
            marketDataFieldQueue.Enqueue(pDepthMarketData);
        }

        private static void PublishQuotation()
        {
            while (true)
            {
                if (marketDataFieldQueue.Count == 0)
                {
                    Thread.Sleep(1);
                    continue;
                }
                ThostFtdcDepthMarketDataField field = marketDataFieldQueue.Dequeue();
                if (field.LastPrice == 0)
                {
                    LogCenter.Log("过滤价格为0的数据：" + field.InstrumentID);
                    continue;
                }
                ArbitrageViewModel.Instance.ProcessQuotation(new Quotation(field));
            }
        }

        #region useless methods
        public override void OnHeartBeatWarning(int nTimeLapse)
        {
            throw new NotImplementedException();
        }
        public override void OnRspError(ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
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