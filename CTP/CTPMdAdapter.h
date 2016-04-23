#pragma once
#include "tradeapidll\ThostFtdcMdApi.h"
#include "Struct.h"
using namespace System;
namespace  Native
{
	class MdSpi;
};
namespace CTP
{
	public ref class CTPMdAdapter
	{
	public:
		CTPMdAdapter();
		CTPMdAdapter(String^ pszFlowPath, bool bIsUsingUdp, bool bIsMulticast);
	private:
		CThostFtdcMdApi* m_pApi;
		Native::MdSpi* m_pSpi;
	public:
		String^ GetApiVersion();
		void Release();
		void Init();
		void Join();
		String^ GetTradingDay();
		void RegisterFront(String^  pszFrontAddress);
		void RegisterNameServer(String^ pszNsAddress);
		void RegisterFensUserInfo(ThostFtdcFensUserInfoField^ pFensUserInfo);
		int SubscribeMarketData(array<String^>^ ppInstrumentID);
		int UnSubscribeMarketData(array<String^>^ ppInstrumentID);
		int SubscribeForQuoteRsp(array<String^>^ ppInstrumentID);
		int UnSubscribeForQuoteRsp(array<String^>^ ppInstrumentID);
		int ReqUserLogin(ThostFtdcReqUserLoginField^ pReqUserLoginField, int nRequestID);
		int ReqUserLogout(ThostFtdcUserLogoutField^ pUserLogout, int nRequestID);
	public:
		virtual void OnFrontConnected() = 0;
		virtual void OnFrontDisconnected(int nReason) = 0;
		virtual void OnHeartBeatWarning(int nTimeLapse) = 0;
		virtual void OnRspUserLogin(ThostFtdcRspUserLoginField^ pRspUserLogin, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspUserLogout(ThostFtdcUserLogoutField^ pUserLogout, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspError(ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspSubMarketData(ThostFtdcSpecificInstrumentField^ pSpecificInstrument, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspUnSubMarketData(ThostFtdcSpecificInstrumentField^ pSpecificInstrument, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspSubForQuoteRsp(ThostFtdcSpecificInstrumentField^ pSpecificInstrument, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRspUnSubForQuoteRsp(ThostFtdcSpecificInstrumentField^ pSpecificInstrument, ThostFtdcRspInfoField^ pRspInfo, int nRequestID, bool bIsLast) = 0;
		virtual void OnRtnDepthMarketData(ThostFtdcDepthMarketDataField^ pDepthMarketData) = 0;
		virtual void OnRtnForQuoteRsp(ThostFtdcForQuoteRspField^ pForQuoteRsp) = 0;
	};
}