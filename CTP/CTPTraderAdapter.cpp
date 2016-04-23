#include "Stdafx.h"
#include "CTPTraderAdapter.h"
#include "TraderSpi.h"
#include "Util.h"
using namespace Native;
using namespace CTP;
CTPTraderAdapter::CTPTraderAdapter()
{
	m_pApi = CThostFtdcTraderApi::CreateFtdcTraderApi();
	m_pSpi = new TraderSpi(this);
	m_pApi->RegisterSpi(m_pSpi);
}
CTPTraderAdapter::CTPTraderAdapter(String^ pszFlowPath)
{
	m_pApi = CThostFtdcTraderApi::CreateFtdcTraderApi(AutoStrPtr(pszFlowPath).m_pChar);
	m_pSpi = new TraderSpi(this);
	m_pApi->RegisterSpi(m_pSpi);
}

void CTPTraderAdapter::Release()
{
	if (m_pApi)
	{
		m_pApi->RegisterSpi(nullptr);
		m_pApi->Release();
		m_pApi = nullptr;
	}
	if (m_pSpi)
	{
		delete m_pSpi;
		m_pSpi = nullptr;
	}
}
void CTPTraderAdapter::Init()
{
	m_pApi->Init();
}
void CTPTraderAdapter::Join()
{
	m_pApi->Join();
}
void CTPTraderAdapter::RegisterFront(String^  pszFrontAddress)
{
	m_pApi->RegisterFront(AutoStrPtr(pszFrontAddress).m_pChar);
}
int CTPTraderAdapter::ReqUserLogin(ThostFtdcReqUserLoginField^ pReqUserLoginField, int nRequestID)
{
	CThostFtdcReqUserLoginField native;
	MToN<ThostFtdcReqUserLoginField^, CThostFtdcReqUserLoginField>(pReqUserLoginField, &native);
	return m_pApi->ReqUserLogin(&native, nRequestID);
}
int CTPTraderAdapter::ReqOrderInsert(ThostFtdcInputOrderField ^ pInputOrder, int nRequestID)
{
	CThostFtdcInputOrderField native;
	MToN<ThostFtdcInputOrderField^, CThostFtdcInputOrderField>(pInputOrder, &native);
	return m_pApi->ReqOrderInsert(&native, nRequestID);
}
int CTPTraderAdapter::ReqSettlementInfoConfirm(ThostFtdcSettlementInfoConfirmField ^ pSettlementInfoConfirm, int nRequestID)
{
	CThostFtdcSettlementInfoConfirmField native;
	MToN<ThostFtdcSettlementInfoConfirmField^, CThostFtdcSettlementInfoConfirmField>(pSettlementInfoConfirm, &native);
	return m_pApi->ReqSettlementInfoConfirm(&native, nRequestID);
}
int CTPTraderAdapter::ReqQryInvestorPosition(ThostFtdcQryInvestorPositionField ^ pQryInvestorPosition, int nRequestID)
{
	CThostFtdcQryInvestorPositionField native;
	MToN<ThostFtdcQryInvestorPositionField^, CThostFtdcQryInvestorPositionField>(pQryInvestorPosition, &native);
	return m_pApi->ReqQryInvestorPosition(&native, nRequestID);
}
int CTPTraderAdapter::ReqQryTradingAccount(ThostFtdcQryTradingAccountField ^ pQryTradingAccount, int nRequestID)
{
	CThostFtdcQryTradingAccountField native;
	MToN<ThostFtdcQryTradingAccountField^, CThostFtdcQryTradingAccountField>(pQryTradingAccount, &native);
	return m_pApi->ReqQryTradingAccount(&native, nRequestID);
}
int CTPTraderAdapter::ReqQryInstrument(ThostFtdcQryInstrumentField^ pQryInstrument, int nRequestID)
{
	CThostFtdcQryInstrumentField native;
	MToN<ThostFtdcQryInstrumentField^, CThostFtdcQryInstrumentField>(pQryInstrument, &native);
	return m_pApi->ReqQryInstrument(&native, nRequestID);
}
int CTPTraderAdapter::ReqQryInvestorPositionDetail(ThostFtdcQryInvestorPositionDetailField ^ pQryInvestorPositionDetail, int nRequestID)
{
	CThostFtdcQryInvestorPositionDetailField native;
	MToN<ThostFtdcQryInvestorPositionDetailField^, CThostFtdcQryInvestorPositionDetailField>(pQryInvestorPositionDetail, &native);
	return m_pApi->ReqQryInvestorPositionDetail(&native, nRequestID);
}