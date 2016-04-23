#include "stdafx.h"
#include "MdSpi.h"
#include "CTPMdAdapter.h"
#include "Util.h"
using namespace CTP;
namespace Native
{
	MdSpi::MdSpi(CTPMdAdapter ^ pAdapter) : m_pAdapter(pAdapter) {	}

	void MdSpi::OnFrontConnected()
	{
		m_pAdapter->OnFrontConnected();
	};

	void MdSpi::OnFrontDisconnected(int nReason)
	{
		m_pAdapter->OnFrontDisconnected(nReason);
	};

	void MdSpi::OnHeartBeatWarning(int nTimeLapse)
	{
		m_pAdapter->OnHeartBeatWarning(nTimeLapse);
	};

	void MdSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcRspUserLoginField^, CThostFtdcRspUserLoginField>(pRspUserLogin);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspUserLogin(field, rspInfo, nRequestID, bIsLast);
	};

	void MdSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcUserLogoutField^, CThostFtdcUserLogoutField>(pUserLogout);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspUserLogout(field, rspInfo, nRequestID, bIsLast);
	};

	void MdSpi::OnRspError(CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspError(NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo), nRequestID, bIsLast);
	};

	void MdSpi::OnRspSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcSpecificInstrumentField^, CThostFtdcSpecificInstrumentField>(pSpecificInstrument);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspSubMarketData(field, rspInfo, nRequestID, bIsLast);
	};

	void MdSpi::OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcSpecificInstrumentField^, CThostFtdcSpecificInstrumentField>(pSpecificInstrument);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspUnSubMarketData(field, rspInfo, nRequestID, bIsLast);
	};

	void MdSpi::OnRspSubForQuoteRsp(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcSpecificInstrumentField^, CThostFtdcSpecificInstrumentField>(pSpecificInstrument);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspSubForQuoteRsp(field, rspInfo, nRequestID, bIsLast);
	}

	void MdSpi::OnRspUnSubForQuoteRsp(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
	{
		auto field = NToM<ThostFtdcSpecificInstrumentField^, CThostFtdcSpecificInstrumentField>(pSpecificInstrument);
		auto rspInfo = NToM<ThostFtdcRspInfoField^, CThostFtdcRspInfoField>(pRspInfo);
		m_pAdapter->OnRspUnSubForQuoteRsp(field, rspInfo, nRequestID, bIsLast);
	}

	void MdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData)
	{
		auto field = NToM<ThostFtdcDepthMarketDataField^, CThostFtdcDepthMarketDataField>(pDepthMarketData);
		m_pAdapter->OnRtnDepthMarketData(field);
	};

	void MdSpi::OnRtnForQuoteRsp(CThostFtdcForQuoteRspField *pForQuoteRsp)
	{
		auto field = NToM<ThostFtdcForQuoteRspField^, CThostFtdcForQuoteRspField>(pForQuoteRsp);
		m_pAdapter->OnRtnForQuoteRsp(field);
	}
}