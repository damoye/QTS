using Host.Common;
using Host.Contract;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Host.DAL
{
    public static class DataSaver
    {
        private static QueueThreadSafe<Quotation> quotationQueue = new QueueThreadSafe<Quotation>();
        private static QueueThreadSafe<Candle> candleQueue = new QueueThreadSafe<Candle>();
        private static DateTime lastTimeToSave;

        static DataSaver()
        {
            var thread = new Thread(Work);
            thread.Name = "DataSaver";
            thread.IsBackground = true;
            thread.Start();
        }

        public static void AddQuotation(Quotation quotation)
        {
            quotationQueue.Enqueue(quotation);
        }

        public static void AddCandle(Candle candle)
        {
            candleQueue.Enqueue(candle);
        }

        public static string GetStatus()
        {
            return "DataSaver's last time to save: " + lastTimeToSave +
                   Environment.NewLine +
                   "DataSaver quotation queue's cout: " + quotationQueue.Count +
                   Environment.NewLine +
                   "DataSaver candle queue's cout: " + candleQueue.Count;
        }

        private static void Work(object obj)
        {
            while (true)
            {
                if (quotationQueue.Count != 0)
                {
                    lastTimeToSave = DateTime.Now;
                    List<Quotation> quotations = quotationQueue.DequeueAll();
                    QuotationDAL.Save(quotations);
                }
                if (candleQueue.Count != 0)
                {
                    lastTimeToSave = DateTime.Now;
                    List<Candle> candles = candleQueue.DequeueAll();
                    CandleDAL.Save(candles);
                }
                Thread.Sleep(1);
            }
        }
    }
}