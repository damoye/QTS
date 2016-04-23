using Host.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Host.DAL
{
    public static class QuotationDAL
    {
        public static void Save(List<Quotation> quotations)
        {
            var builder = new StringBuilder("REPLACE INTO quotation VALUES");
            foreach (var item in quotations)
            {
                builder.Append('(');
                DALUtil.AddField(builder, item.TradingDay.ToString("yyyy-MM-dd"));
                DALUtil.AddField(builder, item.InstrumentID);
                DALUtil.AddField(builder, item.ExchangeID);
                DALUtil.AddField(builder, item.ExchangeInstID);
                DALUtil.AddField(builder, item.LastPrice);
                DALUtil.AddField(builder, item.PreSettlementPrice);
                DALUtil.AddField(builder, item.PreClosePrice);
                DALUtil.AddField(builder, item.PreOpenInterest);
                DALUtil.AddField(builder, item.OpenPrice);
                DALUtil.AddField(builder, item.HighestPrice);
                DALUtil.AddField(builder, item.LowestPrice);
                DALUtil.AddField(builder, item.Volume);
                DALUtil.AddField(builder, item.Turnover);
                DALUtil.AddField(builder, item.OpenInterest);
                DALUtil.AddField(builder, item.ClosePrice);
                DALUtil.AddField(builder, item.SettlementPrice);
                DALUtil.AddField(builder, item.UpperLimitPrice);
                DALUtil.AddField(builder, item.LowerLimitPrice);
                DALUtil.AddField(builder, item.PreDelta);
                DALUtil.AddField(builder, item.CurrDelta);
                DALUtil.AddField(builder, item.Time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                DALUtil.AddField(builder, item.BidPrice1);
                DALUtil.AddField(builder, item.BidVolume1);
                DALUtil.AddField(builder, item.AskPrice1);
                DALUtil.AddField(builder, item.AskVolume1);
                builder.Append(item.AveragePrice);
                builder.Append("),");
            }
            builder.Remove(builder.Length - 1, 1);
            new SQLiteCommand(builder.ToString(), DALUtil.Connection).ExecuteNonQuery();
        }

        public static List<Quotation> Get()
        {
            var table = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM quotation", DALUtil.Connection);
            adapter.Fill(table);
            var quotations = new List<Quotation>();
            foreach (var item in table.Rows)
            {
                var row = item as DataRow;
                var quotation = new Quotation();
                quotation.TradingDay = row.Field<DateTime>(0);
                quotation.InstrumentID = row.Field<string>(1);
                quotation.ExchangeID = row.Field<string>(2);
                quotation.ExchangeInstID = row.Field<string>(3);
                quotation.LastPrice = DALUtil.GetDouble(row[4]);
                quotation.PreSettlementPrice = DALUtil.GetDouble(row[5]);
                quotation.PreClosePrice = DALUtil.GetDouble(row[6]);
                quotation.PreOpenInterest = DALUtil.GetDouble(row[7]);
                quotation.OpenPrice = DALUtil.GetDouble(row[8]);
                quotation.HighestPrice = DALUtil.GetDouble(row[9]);
                quotation.LowestPrice = DALUtil.GetDouble(row[10]);
                quotation.Volume = row.Field<int>(11);
                quotation.Turnover = DALUtil.GetDouble(row[12]);
                quotation.OpenInterest = DALUtil.GetDouble(row[13]);
                quotation.ClosePrice = DALUtil.GetDouble(row[14]);
                quotation.SettlementPrice = DALUtil.GetDouble(row[15]);
                quotation.UpperLimitPrice = DALUtil.GetDouble(row[16]);
                quotation.LowerLimitPrice = DALUtil.GetDouble(row[17]);
                quotation.PreDelta = DALUtil.GetDouble(row[18]);
                quotation.CurrDelta = DALUtil.GetDouble(row[19]);
                quotation.Time = row.Field<DateTime>(20);
                quotation.BidPrice1 = DALUtil.GetDouble(row[21]);
                quotation.BidVolume1 = row.Field<int>(22);
                quotation.AskPrice1 = DALUtil.GetDouble(row[23]);
                quotation.AskVolume1 = row.Field<int>(24);
                quotation.AveragePrice = row.Field<double>(25);
                quotations.Add(quotation);
            }
            return quotations;
        }
    }
}