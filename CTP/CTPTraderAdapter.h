#pragma once
#include "tradeapidll\ThostFtdcTraderApi.h"
#include "Struct.h"
using namespace System;
namespace  Native
{
	class TraderSpi;
};
namespace CTP
{
	public ref class CTPTraderAdapter
	{
	public:
		CTPTraderAdapter();
		CTPTraderAdapter(String^ pszFlowPath);
	private:
		CThostFtdcTraderApi* m_pApi;
		Native::TraderSpi* m_pSpi;
	public:
		void Release();
		void Init();
		void Join();
		void RegisterFront(String^ pszFrontAddress);
		int ReqUserLogin(ThostFtdcReqUserLoginField^ pReqUserLoginField, int nRequestID);
		int ReqOrderInsert(ThostFtdcInputOrderField^ pInputOrder, int nRequestID);
		int ReqSettlementInfoConfirm(ThostFtdcSettlementInfoConfirmField^ pSettlementInfoConfirm, int nRequestID);
		int ReqQryInvestorPosition(ThostFtdcQryInvestorPositionField^ pQryInvestorPosition, int nRequestID);
		int ReqQryTradingAccount(ThostFtdcQryTradingAccountField^ pQryTradingAccount, int nRequestID);
		int ReqQryInstrument(ThostFtdcQryInstrumentField^ pQryInstrument, int nRequestID);
		int ReqQryInvestorPositionDetail(ThostFtdcQryInvestorPositionDetailField^ pQryInvestorPositionDetail, int nRequestID);

		virtual void OnFrontConnected() = 0;
		virtual void OnFrontDisconnected(int nReason) = 0;
		virtual void OnRspUserLogin(ThostFtdcRspUserLoginField^ pRspUserLogin, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspUserLogout(ThostFtdcUserLogoutField^ pUserLogout, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspOrderInsert(ThostFtdcInputOrderField^ pInputOrder, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspQryInvestorPosition(ThostFtdcInvestorPositionField^ pInvestorPosition, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspQryTradingAccount(ThostFtdcTradingAccountField^ pTradingAccount, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspQryInstrument(ThostFtdcInstrumentField^ pInstrument, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspQryInvestorPositionDetail(ThostFtdcInvestorPositionDetailField^ pInvestorPositionDetail, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspError(ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRtnOrder(ThostFtdcOrderField^ pOrder) = 0;
		virtual void OnRtnTrade(ThostFtdcTradeField^ pTrade) = 0;
	};
}