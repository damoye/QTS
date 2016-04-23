#pragma once
#include <vcclr.h>
#include "CTPTraderAdapter.h"
#include "tradeapidll\ThostFtdcTraderApi.h"
namespace Native
{
	class TraderSpi : public CThostFtdcTraderSpi
	{
	public:
		TraderSpi(CTP::CTPTraderAdapter^ pAdapter);
		virtual void OnFrontConnected();
		virtual void OnFrontDisconnected(int nReason);
		virtual void OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspOrderInsert(CThostFtdcInputOrderField *pInputOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspQryInvestorPosition(CThostFtdcInvestorPositionField *pInvestorPosition, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspQryTradingAccount(CThostFtdcTradingAccountField *pTradingAccount, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspQryInstrument(CThostFtdcInstrumentField *pInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspQryInvestorPositionDetail(CThostFtdcInvestorPositionDetailField *pInvestorPositionDetail, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRspError(CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
		virtual void OnRtnOrder(CThostFtdcOrderField *pOrder);
		virtual void OnRtnTrade(CThostFtdcTradeField *pTrade);
	private:
		gcroot<CTP::CTPTraderAdapter^ > m_pAdapter;
	};
};