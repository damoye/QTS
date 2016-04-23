using System;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Host.DAL
{
    public static class DALUtil
    {
        #region sqlScript
        private const string SQL_SCRIPT = @"
CREATE TABLE quotation (
TradingDay DATE,
InstrumentID VARCHAR(31) NOT NULL,
ExchangeID VARCHAR(9),
ExchangeInstID VARCHAR(31),
LastPrice DOUBLE,
PreSettlementPrice DOUBLE,
PreClosePrice DOUBLE,
PreOpenInterest DOUBLE,
OpenPrice DOUBLE,
HighestPrice DOUBLE,
LowestPrice DOUBLE,
Volume INT,
Turnover DOUBLE,
OpenInterest DOUBLE,
ClosePrice DOUBLE,
SettlementPrice DOUBLE,
UpperLimitPrice DOUBLE,
LowerLimitPrice DOUBLE,
PreDelta DOUBLE,
CurrDelta DOUBLE,
Time DATETIME,
BidPrice1 DOUBLE,
BidVolume1 INT,
AskPrice1 DOUBLE,
AskVolume1 INT,
AveragePrice DOUBLE,
PRIMARY KEY(InstrumentID));

CREATE TABLE instrument (
InstrumentID VARCHAR(31) NOT NULL,
ExchangeID VARCHAR(9),
InstrumentName VARCHAR(21),
ExchangeInstID VARCHAR(31),
ProductID VARCHAR(31),
ProductClass TINYINT(4),
DeliveryYear INT,
DeliveryMonth INT,
MaxMarketOrderVolume INT,
MinMarketOrderVolume INT,
MaxLimitOrderVolume INT,
MinLimitOrderVolume INT,
VolumeMultiple INT,
PriceTick DOUBLE,
CreateDate VARCHAR(9),
OpenDate VARCHAR(9),
ExpireDate VARCHAR(9),
StartDelivDate VARCHAR(9),
EndDelivDate VARCHAR(9),
InstLifePhase TINYINT(4),
IsTrading INT,
PositionType TINYINT(4),
PositionDateType TINYINT(4),
LongMarginRatio DOUBLE,
ShortMarginRatio DOUBLE,
MaxMarginSideAlgorithm TINYINT(4),
UnderlyingInstrID VARCHAR(31),
StrikePrice DOUBLE,
OptionsType TINYINT(4),
UnderlyingMultiple DOUBLE,
CombinationType TINYINT(4),
PRIMARY KEY (InstrumentID));

CREATE TABLE candle (
InstrumentID VARCHAR(31) NOT NULL,
TradingDay DATE,
CandleTime DATETIME NOT NULL,
Open DOUBLE,
Close DOUBLE,
High DOUBLE,
Low DOUBLE,
Volume INT,
OpenInterest DOUBLE,
PRIMARY KEY (InstrumentID, CandleTime));";
        #endregion

        public static SQLiteConnection Connection;

        public static async Task Init()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qts.db");
            string connectionString = new SQLiteConnectionStringBuilder { DataSource = path }.ToString();
            bool isExist = File.Exists(path);
            if (isExist)
            {
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
            }
            else
            {
                SQLiteConnection.CreateFile(path);
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
                var cmd = new SQLiteCommand(SQL_SCRIPT, Connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static void AddField(StringBuilder builder, int fieldValue)
        {
            builder.Append(fieldValue);
            builder.Append(',');
        }

        public static void AddField(StringBuilder builder, double fieldValue)
        {
            if (fieldValue == double.MaxValue)
            {
                builder.Append("null");
            }
            else
            {
                builder.Append(fieldValue);
            }
            builder.Append(',');
        }

        public static void AddField(StringBuilder builder, string fieldValue)
        {
            builder.Append('\'');
            builder.Append(fieldValue);
            builder.Append("\',");
        }

        public static void AddLastField(StringBuilder builder, double fieldValue)
        {
            if (fieldValue == double.MaxValue)
            {
                builder.Append("null");
            }
            else
            {
                builder.Append(fieldValue);
            }
        }

        public static double GetDouble(object obj)
        {
            if (obj is DBNull)
            {
                return double.MaxValue;
            }
            else
            {
                return (double)obj;
            }
        }
    }
}