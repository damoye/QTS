#include "Stdafx.h"
#include "CTPMdAdapter.h"
#include "MdSpi.h"
#include "Util.h"
using namespace Native;
using namespace CTP;
CTPMdAdapter::CTPMdAdapter()
{
	m_pApi = CThostFtdcMdApi::CreateFtdcMdApi();
	m_pSpi = new MdSpi(this);
	m_pApi->RegisterSpi(m_pSpi);
}
CTPMdAdapter::CTPMdAdapter(String ^ pszFlowPath, bool bIsUsingUdp, bool bIsMulticast)
{
	AutoStrPtr asp(pszFlowPath);
	m_pApi = CThostFtdcMdApi::CreateFtdcMdApi(asp.m_pChar, bIsUsingUdp, bIsMulticast);
	m_pSpi = new MdSpi(this);
	m_pApi->RegisterSpi(m_pSpi);
}

String^ CTPMdAdapter::GetApiVersion()
{
	return gcnew String(m_pApi->GetApiVersion());
}
void CTPMdAdapter::Release()
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
void CTPMdAdapter::Init()
{
	m_pApi->Init();
}
void CTPMdAdapter::Join()
{
	m_pApi->Join();
}
String^ CTPMdAdapter::GetTradingDay()
{
	return gcnew String(m_pApi->GetTradingDay());
}
void CTPMdAdapter::RegisterFront(String^  pszFrontAddress)
{
	m_pApi->RegisterFront(AutoStrPtr(pszFrontAddress).m_pChar);
}
void CTPMdAdapter::RegisterNameServer(String^  pszNsAddress)
{
	m_pApi->RegisterNameServer(AutoStrPtr(pszNsAddress).m_pChar);
}
void CTPMdAdapter::RegisterFensUserInfo(ThostFtdcFensUserInfoField^ pFensUserInfo)
{
	CThostFtdcFensUserInfoField field;
	MToN<ThostFtdcFensUserInfoField^, CThostFtdcFensUserInfoField>(pFensUserInfo, &field);
	m_pApi->RegisterFensUserInfo(&field);
}
int CTPMdAdapter::SubscribeMarketData(array<String^>^ ppInstrumentID)
{
	AutoStrArrayPtr asap(ppInstrumentID);
	int result = m_pApi->SubscribeMarketData(asap.ppChar, ppInstrumentID->Length);
	return result;
}
int CTPMdAdapter::UnSubscribeMarketData(array<String^>^ ppInstrumentID)
{
	AutoStrArrayPtr asap(ppInstrumentID);
	int result = m_pApi->UnSubscribeMarketData(asap.ppChar, ppInstrumentID->Length);
	return result;
}
int CTPMdAdapter::SubscribeForQuoteRsp(array<String^>^ ppInstrumentID)
{
	AutoStrArrayPtr asap(ppInstrumentID);
	int result = m_pApi->SubscribeForQuoteRsp(asap.ppChar, ppInstrumentID->Length);
	return result;
}
int CTPMdAdapter::UnSubscribeForQuoteRsp(array<String^>^ ppInstrumentID)
{
	AutoStrArrayPtr asap(ppInstrumentID);
	int result = m_pApi->UnSubscribeForQuoteRsp(asap.ppChar, ppInstrumentID->Length);
	return result;
}
int CTPMdAdapter::ReqUserLogin(ThostFtdcReqUserLoginField^ pReqUserLoginField, int nRequestID)
{
	CThostFtdcReqUserLoginField native;
	MToN<ThostFtdcReqUserLoginField^, CThostFtdcReqUserLoginField>(pReqUserLoginField, &native);
	return m_pApi->ReqUserLogin(&native, nRequestID);
}
int CTPMdAdapter::ReqUserLogout(ThostFtdcUserLogoutField^ pUserLogout, int nRequestID)
{
	CThostFtdcUserLogoutField native;
	MToN<ThostFtdcUserLogoutField^, CThostFtdcUserLogoutField>(pUserLogout, &native);
	return m_pApi->ReqUserLogout(&native, nRequestID);
}

