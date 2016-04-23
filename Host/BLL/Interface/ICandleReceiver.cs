using Host.Contract;

namespace Host.BLL.Interface
{
    public interface ICandleReceiver
    {
        void ProcessCandle(Candle candle);
    }
}
