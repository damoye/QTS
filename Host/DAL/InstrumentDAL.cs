using CTP;
using Host.Contract;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Linq;

namespace Host.DAL
{
    public static class InstrumentDAL
    {
        public static void Save(Instrument[] instrument)
        {
            new SQLiteCommand("DELETE FROM `instrument`", DALUtil.Connection).ExecuteNonQuery();
            var builder = new StringBuilder("REPLACE INTO `instrument` VALUES");
            foreach (var item in instrument)
            {
                builder.Append('(');
                DALUtil.AddField(builder, item.InstrumentID);
                DALUtil.AddField(builder, item.ExchangeID);
                DALUtil.AddField(builder, item.InstrumentName);
                DALUtil.AddField(builder, item.ExchangeInstID);
                DALUtil.AddField(builder, item.ProductID);
                DALUtil.AddField(builder, (int)item.ProductClass);
                DALUtil.AddField(builder, item.DeliveryYear);
                DALUtil.AddField(builder, item.DeliveryMonth);
                DALUtil.AddField(builder, item.MaxMarketOrderVolume);
                DALUtil.AddField(builder, item.MinMarketOrderVolume);
                DALUtil.AddField(builder, item.MaxLimitOrderVolume);
                DALUtil.AddField(builder, item.MinLimitOrderVolume);
                DALUtil.AddField(builder, item.VolumeMultiple);
                DALUtil.AddField(builder, item.PriceTick);
                DALUtil.AddField(builder, item.CreateDate);
                DALUtil.AddField(builder, item.OpenDate);
                DALUtil.AddField(builder, item.ExpireDate);
                DALUtil.AddField(builder, item.StartDelivDate);
                DALUtil.AddField(builder, item.EndDelivDate);
                DALUtil.AddField(builder, (int)item.InstLifePhase);
                DALUtil.AddField(builder, item.IsTrading);
                DALUtil.AddField(builder, (int)item.PositionType);
                DALUtil.AddField(builder, (int)item.PositionDateType);
                DALUtil.AddField(builder, item.LongMarginRatio);
                DALUtil.AddField(builder, item.ShortMarginRatio);
                DALUtil.AddField(builder, (int)item.MaxMarginSideAlgorithm);
                DALUtil.AddField(builder, item.UnderlyingInstrID);
                DALUtil.AddField(builder, item.StrikePrice);
                DALUtil.AddField(builder, (int)item.OptionsType);
                DALUtil.AddField(builder, item.UnderlyingMultiple);
                builder.Append((int)item.CombinationType);
                builder.Append("),");
            }
            builder.Remove(builder.Length - 1, 1);
            new SQLiteCommand(builder.ToString(), DALUtil.Connection).ExecuteNonQuery();
        }

        public static Instrument[] Get()
        {
            var table = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM instrument", DALUtil.Connection);
            adapter.Fill(table);
            var instruments = new List<Instrument>();
            foreach (var item in table.Rows)
            {
                var row = item as DataRow;
                var instrument = new Instrument();
                instrument.InstrumentID = row.Field<string>(0);
                instrument.ExchangeID = row.Field<string>(1);
                instrument.InstrumentName = row.Field<string>(2);
                instrument.ExchangeInstID = row.Field<string>(3);
                instrument.ProductID = row.Field<string>(4);
                instrument.ProductClass = (EnumProductClassType)row.Field<byte>(5);
                instrument.DeliveryYear = row.Field<int>(6);
                instrument.DeliveryMonth = row.Field<int>(7);
                instrument.MaxMarketOrderVolume = row.Field<int>(8);
                instrument.MinMarketOrderVolume = row.Field<int>(9);
                instrument.MaxLimitOrderVolume = row.Field<int>(10);
                instrument.MinLimitOrderVolume = row.Field<int>(11);
                instrument.VolumeMultiple = row.Field<int>(12);
                instrument.PriceTick = row.Field<double>(13);
                instrument.CreateDate = row.Field<string>(14);
                instrument.OpenDate = row.Field<string>(15);
                instrument.ExpireDate = row.Field<string>(16);
                instrument.StartDelivDate = row.Field<string>(17);
                instrument.EndDelivDate = row.Field<string>(18);
                instrument.InstLifePhase = (EnumInstLifePhaseType)row.Field<byte>(19);
                instrument.IsTrading = row.Field<int>(20);
                instrument.PositionType = (EnumPositionTypeType)row.Field<byte>(21);
                instrument.PositionDateType = (EnumPositionDateTypeType)row.Field<byte>(22);
                instrument.LongMarginRatio = row.Field<double>(23);
                instrument.ShortMarginRatio = row.Field<double>(24);
                instrument.MaxMarginSideAlgorithm = (EnumMaxMarginSideAlgorithmType)row.Field<byte>(25);
                instrument.UnderlyingInstrID = row.Field<string>(26);
                instrument.StrikePrice = DALUtil.GetDouble(row[27]);
                instrument.OptionsType = (EnumOptionsTypeType)row.Field<byte>(28);
                instrument.UnderlyingMultiple = DALUtil.GetDouble(row[29]);
                instrument.CombinationType = (EnumCombinationTypeType)row.Field<byte>(30);
                instruments.Add(instrument);
            }
            return instruments.OrderBy(p => p.InstrumentID).ToArray();
        }
    }
}
