using FutureArbitrage.Contract;
using FutureArbitrage.PL;

namespace FutureArbitrage.BLL
{
    public static class LogCenter
    {
        public static void Log(string content)
        {
            LogViewModel.Instance.AddLog(new Log(content));
        }

        public static void Log(string format, params object[] args)
        {
            Log(string.Format(format, args));
        }

        public static void Error(string content)
        {
            Log(content);
            MainViewModel.Instance.Alert(content);
        }

        public static void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }
    }
}