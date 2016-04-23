#include "StdAfx.h"
#include "TraderSpi.h"
#include "CTPTraderAdapter.h"
#include "Struct.h"
#include "Util.h"
using namespace CTP;
namespace Native
{
	TraderSpi::TraderSpi(CTP::CTPTraderAdapter^ pAdapter)
	{
		m_pAdapter = pAdapter;
	}
	void TraderSpi::OnFrontConnected()
	{
		m_pAdapter->OnFrontConnected();
	};
	void TraderSpi::OnFrontDisconnected(int nReason)
	{
		m_pAdapter->OnFrontDisconnected(nReason);
	};
	void TraderSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcRspUserLoginField^, CThostFtdcRspUserLoginField>(pRspUserLogin);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspUserLogin(field, rspInfo, nRequestID, bIsLast);
	};
	void TraderSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcUserLogoutField^, CThostFtdcUserLogoutField>(pUserLogout);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspUserLogout(field, rspInfo, nRequestID, bIsLast);
	};
	void TraderSpi::OnRspOrderInsert(CThostFtdcInputOrderField *pInputOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcInputOrderField^, CThostFtdcInputOrderField>(pInputOrder);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspOrderInsert(field, rspInfo, nRequestID, bIsLast);
	}
	void TraderSpi::OnRspQryInvestorPosition(CThostFtdcInvestorPositionField *pInvestorPosition, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcInvestorPositionField^, CThostFtdcInvestorPositionField>(pInvestorPosition);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspQryInvestorPosition(field, rspInfo, nRequestID, bIsLast);
	}
	void TraderSpi::OnRspQryTradingAccount(CThostFtdcTradingAccountField * pTradingAccount, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcTradingAccountField^, CThostFtdcTradingAccountField>(pTradingAccount);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspQryTradingAccount(field, rspInfo, nRequestID, bIsLast);
	}
	void TraderSpi::OnRspQryInstrument(CThostFtdcInstrumentField *pInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcInstrumentField^, CThostFtdcInstrumentField>(pInstrument);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspQryInstrument(field, rspInfo, nRequestID, bIsLast);
	}
	void TraderSpi::OnRspQryInvestorPositionDetail(CThostFtdcInvestorPositionDetailField *pInvestorPositionDetail, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcInvestorPositionDetailField^, CThostFtdcInvestorPositionDetailField>(pInvestorPositionDetail);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspQryInvestorPositionDetail(field, rspInfo, nRequestID, bIsLast);
	}
	void TraderSpi::OnRspError(CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspError(field, nRequestID, bIsLast);
	}
	void TraderSpi::OnRtnOrder(CThostFtdcOrderField * pOrder)
	{
		auto field = NToM<ThostFtdcOrderField^, CThostFtdcOrderField>(pOrder);
		m_pAdapter->OnRtnOrder(field);
	}
	void TraderSpi::OnRtnTrade(CThostFtdcTradeField * pTrade)
	{
		auto field = NToM<ThostFtdcTradeField^, CThostFtdcTradeField>(pTrade);
		m_pAdapter->OnRtnTrade(field);
	}
}