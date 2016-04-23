using Host.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Host.DAL
{
    public static class CandleDAL
    {
        public static void Save(List<Candle> candles)
        {
            var builder = new StringBuilder("REPLACE INTO candle VALUES");
            foreach (var item in candles)
            {
                builder.Append('(');
                DALUtil.AddField(builder, item.InstrumentID);
                DALUtil.AddField(builder, item.TradingDay.ToString("yyyy-MM-dd"));
                DALUtil.AddField(builder, item.CandleTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                DALUtil.AddField(builder, item.Open);
                DALUtil.AddField(builder, item.Close);
                DALUtil.AddField(builder, item.High);
                DALUtil.AddField(builder, item.Low);
                DALUtil.AddField(builder, item.Volume);
                DALUtil.AddLastField(builder, item.OpenInterest);
                builder.Append("),");
            }
            builder.Remove(builder.Length - 1, 1);
            new SQLiteCommand(builder.ToString(), DALUtil.Connection).ExecuteNonQuery();
        }

        public static Candle GetLast(string instrumentID)
        {
            var command = new SQLiteCommand("SELECT * FROM candle WHERE instrumentid=@InstrumentID order by candletime desc limit 1", DALUtil.Connection);
            command.Parameters.AddWithValue("@InstrumentID", instrumentID);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            var candle = new Candle();
            candle.InstrumentID = reader.GetString(0);
            candle.TradingDay = reader.GetDateTime(1);
            candle.CandleTime = reader.GetDateTime(2);
            candle.Open = DALUtil.GetDouble(reader[3]);
            candle.Close = DALUtil.GetDouble(reader[4]);
            candle.High = DALUtil.GetDouble(reader[5]);
            candle.Low = DALUtil.GetDouble(reader[6]);
            candle.Volume = reader.GetInt32(7);
            candle.OpenInterest = DALUtil.GetDouble(reader[8]);
            return candle;
        }

        public static List<Candle> Get(string instrumentID, DateTime fromTradingDay)
        {
            var table = new DataTable();
            var command = new SQLiteCommand("SELECT * FROM candle WHERE instrumentid=@InstrumentID AND tradingday>=@TradingDay", DALUtil.Connection);
            command.Parameters.AddWithValue("@InstrumentID", instrumentID);
            command.Parameters.AddWithValue("@TradingDay", fromTradingDay);
            var adapter = new SQLiteDataAdapter(command);
            adapter.Fill(table);
            var candles = new List<Candle>();
            foreach (var item in table.Rows)
            {
                var row = item as DataRow;
                var candle = new Candle();
                candle.InstrumentID = row.Field<string>(0);
                candle.TradingDay = row.Field<DateTime>(1);
                candle.CandleTime = row.Field<DateTime>(2);
                candle.Open = DALUtil.GetDouble(row[3]);
                candle.Close = DALUtil.GetDouble(row[4]);
                candle.High = DALUtil.GetDouble(row[5]);
                candle.Low = DALUtil.GetDouble(row[6]);
                candle.Volume = row.Field<int>(7);
                candle.OpenInterest = DALUtil.GetDouble(row[8]);
                candles.Add(candle);
            }
            return candles;
        }
    }
}