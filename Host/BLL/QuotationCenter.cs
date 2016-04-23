using CTP;
using Host.BLL.Interface;
using Host.Contract;
using Host.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.BLL
{
    public static class QuotationCenter
    {
        private static Dictionary<string, Quotation> quotationDic = new Dictionary<string, Quotation>();
        private static Dictionary<string, List<IQuotationReceiver>> subscribeDic = new Dictionary<string, List<IQuotationReceiver>>();

        public static void Init()
        {
            List<Quotation> quotations = QuotationDAL.Get();
            foreach (var item in quotations)
            {
                quotationDic[item.InstrumentID] = item;
            }
        }

        public static void ProcessMarketDataField(ThostFtdcDepthMarketDataField field)
        {
            var quotation = new Quotation(field);
            int volume = 0;
            Quotation preQuotation;
            if (quotationDic.TryGetValue(quotation.InstrumentID, out preQuotation) && preQuotation.TradingDay == quotation.TradingDay)
            {
                volume = quotation.Volume - preQuotation.Volume;
            }
            quotationDic[quotation.InstrumentID] = quotation;
            CandleCenter.ProcessQuotation(quotation, volume);
            DataSaver.AddQuotation(quotation);
            IQuotationReceiver[] receivers;
            lock (subscribeDic)
            {
                List<IQuotationReceiver> receiverList;
                if (!subscribeDic.TryGetValue(quotation.InstrumentID, out receiverList))
                {
                    return;
                }
                receivers = receiverList.ToArray();
            }
            foreach (var item in receivers)
            {
                item.ProcessQuotation(quotation, volume);
            }
        }

        public static Quotation GetQuotation(string instrumentID)
        {
            Quotation quotation;
            quotationDic.TryGetValue(instrumentID, out quotation);
            return quotation;
        }

        public static void Subscribe(string instrumentID, IQuotationReceiver receiver)
        {
            lock (subscribeDic)
            {
                List<IQuotationReceiver> receivers;
                if (!subscribeDic.TryGetValue(instrumentID, out receivers))
                {
                    receivers = new List<IQuotationReceiver>() { receiver };
                    subscribeDic.Add(instrumentID, receivers);
                    return;
                }
                if (!receivers.Contains(receiver))
                {
                    receivers.Add(receiver);
                }
            }
        }

        public static void UnSubscribe(string instrumentID, IQuotationReceiver receiver)
        {
            lock (subscribeDic)
            {
                DoUnsub(instrumentID, receiver);
            }
        }

        public static void UnSubscribe(IEnumerable<string> instrumentIDs, IQuotationReceiver receiver)
        {
            lock (subscribeDic)
            {
                foreach (var instrumentID in instrumentIDs)
                {
                    DoUnsub(instrumentID, receiver);
                }
            }
        }

        private static void DoUnsub(string instrumentID, IQuotationReceiver receiver)
        {
            List<IQuotationReceiver> receivers;
            if (!subscribeDic.TryGetValue(instrumentID, out receivers))
            {
                return;
            }
            receivers.Remove(receiver);
            if (receivers.Count != 0)
            {
                return;
            }
            subscribeDic.Remove(instrumentID);
        }
    }
}