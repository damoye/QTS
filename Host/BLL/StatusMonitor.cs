using Host.BLL.CTP;
using Host.DAL;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Host.BLL
{
    public static class StatusMonitor
    {
        private static Timer timer;

        public static void Start()
        {
            timer = new Timer(Report, null, 0, 5000);
        }

        private static void Report(object state)
        {
            var builder = new StringBuilder();
            builder.AppendLine("----------Status Report:" + DateTime.Now);
            builder.AppendLine(TradeAdapter.Instance.GetStatus());
            builder.AppendLine(MdAdapter.Instance.GetStatus());
            builder.AppendLine(DataSaver.GetStatus());
            builder.AppendLine("----------Status End----------");
            Debug.WriteLine(builder.ToString());
        }
    }
}
