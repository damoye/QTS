using CTP;
using FutureArbitrage.BLL;
using FutureArbitrage.Contract;
using FutureArbitrage.PL;
using FutureArbitrage.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FutureArbitrage.CTP
{
    public class TradeAdapter : CTPTraderAdapter
    {
        private List<Instrument> instruments = new List<Instrument>();
        private List<InvestorPosition> positions = new List<InvestorPosition>();
        private List<InvestorPositionDetail> positionDetails = new List<InvestorPositionDetail>();
        private static TradeAdapter instance;
        public static TradeAdapter Instance { get { return instance; } }
        private TradeAdapter() { }

        public static void ChangeAccount()
        {
            AccountCenter.Instance.IsTDLogin = false;
            if (instance != null)
            {
                instance.Release();
            }
            instance = new TradeAdapter();
            instance.RegisterFront("tcp://" + AccountCenter.Instance.Account.TDAddress);
            instance.Init();
        }

        public override void OnFrontConnected()
        {
            LogCenter.Log("交易连接成功");
            var loginField = new ThostFtdcReqUserLoginField
            {
                BrokerID = AccountCenter.Instance.Account.BrokerID,
                UserID = AccountCenter.Instance.Account.InvestorID,
                Password = AccountCenter.Instance.Account.Password
            };
            int i = this.ReqUserLogin(loginField, 0);
            if (i != 0)
            {
                LogCenter.Error("交易登录失败：" + CTPErrorHelper.GetError(i));
            }
        }

        public override void OnFrontDisconnected(int nReason)
        {
            LogCenter.Error("交易断开：{0} {1}", nReason, CTPErrorHelper.GetFrontDisconnectedReason(nReason));
            AccountCenter.Instance.IsTDLogin = false;
        }

        public override void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("交易登录失败：" + pRspInfo.ErrorMsg);
                return;
            }
            LogCenter.Log("交易登录成功");
            AccountCenter.Instance.IsTDLogin = true;
            this.instruments.Clear();
            LogCenter.Log("请求查询合约");
            int i = this.ReqQryInstrument(new ThostFtdcQryInstrumentField(), 0);
            if (i != 0)
            {
                LogCenter.Error("请求查询合约错误：" + CTPErrorHelper.GetError(i));
            }
            var field = new ThostFtdcSettlementInfoConfirmField
            {
                BrokerID = AccountCenter.Instance.Account.BrokerID,
                InvestorID = AccountCenter.Instance.Account.InvestorID,
                ConfirmDate = DateTime.Today.ToString(),
                ConfirmTime = DateTime.Now.ToLongTimeString()
            };
            LogCenter.Log("投资者结算结果确认");
            i = this.ReqSettlementInfoConfirm(field, 0);
            if (i != 0)
            {
                LogCenter.Error("投资者结算结果确认错误：" + CTPErrorHelper.GetError(i));
            }
        }

        public override void OnRspUserLogout(ThostFtdcUserLogoutField pUserLogout, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            throw new NotImplementedException();
        }

        public override void OnRspOrderInsert(ThostFtdcInputOrderField pInputOrder, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            LogCenter.Error("报单录入请求响应：{0} {1}", pRspInfo.ErrorMsg, pRspInfo.ErrorID);
        }

        public override void OnRspQryInvestorPosition(ThostFtdcInvestorPositionField pInvestorPosition, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo != null && pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("请求查询投资者持仓错误：" + pRspInfo.ErrorMsg);
                return;
            }
            if (pInvestorPosition == null)
            {
                return;
            }
            var position = new InvestorPosition
            {
                InstrumentID = pInvestorPosition.InstrumentID,
                BrokerID = pInvestorPosition.BrokerID,
                InvestorID = pInvestorPosition.InvestorID,
                PosiDirection = (PosiDirection)pInvestorPosition.PosiDirection,
                HedgeFlag = (HedgeFlag)pInvestorPosition.HedgeFlag,
                PositionDate = (PositionDateType)pInvestorPosition.PositionDate,
                YdPosition = pInvestorPosition.YdPosition,
                Position = pInvestorPosition.Position,
                LongFrozen = pInvestorPosition.LongFrozen,
                ShortFrozen = pInvestorPosition.ShortFrozen,
                LongFrozenAmount = pInvestorPosition.LongFrozenAmount,
                ShortFrozenAmount = pInvestorPosition.ShortFrozenAmount,
                OpenVolume = pInvestorPosition.OpenVolume,
                CloseVolume = pInvestorPosition.CloseVolume,
                OpenAmount = pInvestorPosition.OpenAmount,
                CloseAmount = pInvestorPosition.CloseAmount,
                PositionCost = pInvestorPosition.PositionCost,
                PreMargin = pInvestorPosition.PreMargin,
                UseMargin = pInvestorPosition.UseMargin,
                FrozenMargin = pInvestorPosition.FrozenMargin,
                FrozenCash = pInvestorPosition.FrozenCash,
                FrozenCommission = pInvestorPosition.FrozenCommission,
                CashIn = pInvestorPosition.CashIn,
                Commission = pInvestorPosition.Commission,
                CloseProfit = pInvestorPosition.CloseProfit,
                PositionProfit = pInvestorPosition.PositionProfit,
                PreSettlementPrice = pInvestorPosition.PreSettlementPrice,
                SettlementPrice = pInvestorPosition.SettlementPrice,
                TradingDay = pInvestorPosition.TradingDay,
                SettlementID = pInvestorPosition.SettlementID,
                OpenCost = pInvestorPosition.OpenCost,
                ExchangeMargin = pInvestorPosition.ExchangeMargin,
                CombPosition = pInvestorPosition.CombPosition,
                CombLongFrozen = pInvestorPosition.CombLongFrozen,
                CombShortFrozen = pInvestorPosition.CombShortFrozen,
                CloseProfitByDate = pInvestorPosition.CloseProfitByDate,
                CloseProfitByTrade = pInvestorPosition.CloseProfitByTrade,
                TodayPosition = pInvestorPosition.TodayPosition,
                MarginRateByMoney = pInvestorPosition.MarginRateByMoney,
                MarginRateByVolume = pInvestorPosition.MarginRateByVolume,
                StrikeFrozen = pInvestorPosition.StrikeFrozen,
                StrikeFrozenAmount = pInvestorPosition.StrikeFrozenAmount,
                AbandonFrozen = pInvestorPosition.AbandonFrozen,
            };
            this.positions.Add(position);
            if (bIsLast)
            {
                PositionViewModel.Instance.Update(this.positions);
            }
        }

        public override void OnRspQryTradingAccount(ThostFtdcTradingAccountField pTradingAccount, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo != null && pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("请求查询资金账户错误：" + pRspInfo.ErrorMsg);
                return;
            }
            if (pTradingAccount == null)
            {
                LogCenter.Error("请求查询资金账户错误，返回值为空");
                return;
            }
            var tradingAccount = new TradingAccount
            {
                BrokerID = pTradingAccount.BrokerID,
                AccountID = pTradingAccount.AccountID,
                PreMortgage = pTradingAccount.PreMortgage,
                PreCredit = pTradingAccount.PreCredit,
                PreDeposit = pTradingAccount.PreDeposit,
                PreBalance = pTradingAccount.PreBalance,
                PreMargin = pTradingAccount.PreMargin,
                InterestBase = pTradingAccount.InterestBase,
                Interest = pTradingAccount.Interest,
                Deposit = pTradingAccount.Deposit,
                Withdraw = pTradingAccount.Withdraw,
                FrozenMargin = pTradingAccount.FrozenMargin,
                FrozenCash = pTradingAccount.FrozenCash,
                FrozenCommission = pTradingAccount.FrozenCommission,
                CurrMargin = pTradingAccount.CurrMargin,
                CashIn = pTradingAccount.CashIn,
                Commission = pTradingAccount.Commission,
                CloseProfit = pTradingAccount.CloseProfit,
                PositionProfit = pTradingAccount.PositionProfit,
                Balance = pTradingAccount.Balance,
                Available = pTradingAccount.Available,
                WithdrawQuota = pTradingAccount.WithdrawQuota,
                Reserve = pTradingAccount.Reserve,
                TradingDay = pTradingAccount.TradingDay,
                SettlementID = pTradingAccount.SettlementID,
                Credit = pTradingAccount.Credit,
                Mortgage = pTradingAccount.Mortgage,
                ExchangeMargin = pTradingAccount.ExchangeMargin,
                DeliveryMargin = pTradingAccount.DeliveryMargin,
                ExchangeDeliveryMargin = pTradingAccount.ExchangeDeliveryMargin,
                ReserveBalance = pTradingAccount.ReserveBalance,
                CurrencyID = pTradingAccount.CurrencyID,
                PreFundMortgageIn = pTradingAccount.PreFundMortgageIn,
                PreFundMortgageOut = pTradingAccount.PreFundMortgageOut,
                FundMortgageIn = pTradingAccount.FundMortgageIn,
                FundMortgageOut = pTradingAccount.FundMortgageOut,
                FundMortgageAvailable = pTradingAccount.FundMortgageAvailable,
                MortgageableFund = pTradingAccount.MortgageableFund,
                SpecProductMargin = pTradingAccount.SpecProductMargin,
                SpecProductFrozenMargin = pTradingAccount.SpecProductFrozenMargin,
                SpecProductCommission = pTradingAccount.SpecProductCommission,
                SpecProductFrozenCommission = pTradingAccount.SpecProductFrozenCommission,
                SpecProductPositionProfit = pTradingAccount.SpecProductPositionProfit,
                SpecProductCloseProfit = pTradingAccount.SpecProductCloseProfit,
                SpecProductPositionProfitByAlg = pTradingAccount.SpecProductPositionProfitByAlg,
                SpecProductExchangeMargin = pTradingAccount.SpecProductExchangeMargin,
            };
            TradingAccountViewModel.Instance.Update(tradingAccount);
        }

        public override void OnRspQryInstrument(ThostFtdcInstrumentField pInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo != null && pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("请求查询合约错误：" + pRspInfo.ErrorMsg);
                return;
            }
            if (pInstrument.ProductClass == EnumProductClassType.Futures)
            {
                this.instruments.Add(new Instrument(pInstrument));
            }
            if (bIsLast)
            {
                LogCenter.Log("收到期货合约数量：" + this.instruments.Count);
                InstrumentCenter.Instance.SetInstruments(this.instruments.OrderBy(p => p.InstrumentID).ToArray());
            }
        }

        public override void OnRspQryInvestorPositionDetail(ThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (pRspInfo != null && pRspInfo.ErrorID != 0)
            {
                LogCenter.Error("查询投资者持仓明细错误：" + pRspInfo.ErrorMsg);
                return;
            }
            if (pInvestorPositionDetail == null)
            {
                return;
            }
            var detial = new InvestorPositionDetail
            {
                InstrumentID = pInvestorPositionDetail.InstrumentID,
                BrokerID = pInvestorPositionDetail.BrokerID,
                InvestorID = pInvestorPositionDetail.InvestorID,
                HedgeFlag = (HedgeFlag)pInvestorPositionDetail.HedgeFlag,
                Direction = (DirectionType)pInvestorPositionDetail.Direction,
                OpenDate = pInvestorPositionDetail.OpenDate,
                TradeID = pInvestorPositionDetail.TradeID,
                Volume = pInvestorPositionDetail.Volume,
                OpenPrice = pInvestorPositionDetail.OpenPrice,
                TradingDay = pInvestorPositionDetail.TradingDay,
                SettlementID = pInvestorPositionDetail.SettlementID,
                TradeType = (TradeType)pInvestorPositionDetail.TradeType,
                CombInstrumentID = pInvestorPositionDetail.CombInstrumentID,
                ExchangeID = pInvestorPositionDetail.ExchangeID,
                CloseProfitByDate = pInvestorPositionDetail.CloseProfitByDate,
                CloseProfitByTrade = pInvestorPositionDetail.CloseProfitByTrade,
                PositionProfitByDate = pInvestorPositionDetail.PositionProfitByDate,
                PositionProfitByTrade = pInvestorPositionDetail.PositionProfitByTrade,
                Margin = pInvestorPositionDetail.Margin,
                ExchMargin = pInvestorPositionDetail.ExchMargin,
                MarginRateByMoney = pInvestorPositionDetail.MarginRateByMoney,
                MarginRateByVolume = pInvestorPositionDetail.MarginRateByVolume,
                LastSettlementPrice = pInvestorPositionDetail.LastSettlementPrice,
                SettlementPrice = pInvestorPositionDetail.SettlementPrice,
                CloseVolume = pInvestorPositionDetail.CloseVolume,
                CloseAmount = pInvestorPositionDetail.CloseAmount,
            };
            this.positionDetails.Add(detial);
            if (bIsLast)
            {
                PositionViewModel.Instance.Update(this.positionDetails);
            }
        }

        public override void OnRspError(ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            LogCenter.Error("交易错误：{0} {1}", pRspInfo.ErrorMsg, pRspInfo.ErrorID);
        }

        public override void OnRtnOrder(ThostFtdcOrderField pOrder)
        {
            var order = new Order
            {
                InstrumentID = pOrder.InstrumentID,
                UserID = pOrder.UserID,
                Direction = (DirectionType)pOrder.Direction,
                CombOffsetFlag = (OffsetFlag)pOrder.CombOffsetFlag_0,
                LimitPrice = pOrder.LimitPrice,
                VolumeTotalOriginal = pOrder.VolumeTotalOriginal,
                OrderSubmitStatus = (OrderSubmitStatus)pOrder.OrderSubmitStatus,
                OrderStatus = (OrderStatus)pOrder.OrderStatus,
                VolumeTraded = pOrder.VolumeTraded,
                VolumeTotal = pOrder.VolumeTotal,
                InsertTime = DateTimeHelper.GenerateTime(pOrder.InsertDate, pOrder.InsertTime),
                StatusMsg = pOrder.StatusMsg,
            };
            OrderRecordViewModel.Instance.AddOrder(order);
        }

        public override void OnRtnTrade(ThostFtdcTradeField pTrade)
        {
            var trade = new Trade
            {
                InstrumentID = pTrade.InstrumentID,
                UserID = pTrade.UserID,
                Direction = (DirectionType)pTrade.Direction,
                OffsetFlag = (OffsetFlag)pTrade.OffsetFlag,
                Price = pTrade.Price,
                Volume = pTrade.Volume,
                TradeTime = DateTimeHelper.GenerateTime(pTrade.TradeDate, pTrade.TradeTime),
            };
            TradeRecordViewModel.Instance.AddTrade(trade);
        }

        public void InsertOrder(InputOrder order)
        {
            LogCenter.Log("下单：" + order.ToString());
            var field = new ThostFtdcInputOrderField
            {
                BrokerID = order.BrokerID,
                InvestorID = order.InvestorID,
                Direction = (EnumDirectionType)order.Direction,
                CombOffsetFlag_0 = (EnumOffsetFlagType)order.OffsetFlag,
                InstrumentID = order.InstrumentID,
                LimitPrice = order.Price,
                VolumeTotalOriginal = order.Volume,
                UserID = order.GroupID,

                CombHedgeFlag_0 = EnumHedgeFlagType.Speculation,
                ForceCloseReason = EnumForceCloseReasonType.NotForceClose,
                ContingentCondition = EnumContingentConditionType.Immediately,
                TimeCondition = EnumTimeConditionType.GFD,
                VolumeCondition = EnumVolumeConditionType.AV,
                OrderPriceType = EnumOrderPriceTypeType.LimitPrice,
                MinVolume = 1,
                IsAutoSuspend = 1,
            };
            int i = this.ReqOrderInsert(field, 0);
            if (i != 0)
            {
                LogCenter.Error("输入报单错误：" + CTPErrorHelper.GetError(i));
            }
        }

        public void ReqPosition()
        {
            this.positions.Clear();
            var qryPositionField = new ThostFtdcQryInvestorPositionField();
            int i = this.ReqQryInvestorPosition(qryPositionField, 0);
            if (i != 0)
            {
                LogCenter.Error("查询投资者持仓错误：" + CTPErrorHelper.GetError(i));
            }
        }

        public void ReqPositionDetial()
        {
            this.positionDetails.Clear();
            var qryPositionDetialField = new ThostFtdcQryInvestorPositionDetailField();
            int i = this.ReqQryInvestorPositionDetail(qryPositionDetialField, 0);
            if (i != 0)
            {
                LogCenter.Error("查询投资者持仓明细错误：" + CTPErrorHelper.GetError(i));
            }
        }

        public void ReqTradingAccount()
        {
            var field = new ThostFtdcQryTradingAccountField();
            int i = this.ReqQryTradingAccount(field, 0);
            if (i != 0)
            {
                LogCenter.Error("查询资金账户错误：" + CTPErrorHelper.GetError(i));
            }
        }
    }
}
