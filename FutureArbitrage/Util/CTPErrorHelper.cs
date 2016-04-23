namespace FutureArbitrage.Util
{
    public static class CTPErrorHelper
    {
        public static string GetError(int value)
        {
            if (value == -2)
            {
                return "未处理请求超过许可数";
            }
            else if (value == -3)
            {
                return "每秒发送请求数超过许可数";
            }
            else
            {
                return value.ToString();
            }
        }

        public static string GetFrontDisconnectedReason(int nReason)
        {
            switch (nReason)
            {
                case 0x1001:
                    return "网络读失败";
                case 0x1002:
                    return "网络写失败";
                case 0x2001:
                    return "接收心跳超时";
                case 0x2002:
                    return "发送心跳失败";
                case 0x2003:
                    return "收到错误报文";
                default:
                    return "未知";
            }
        }
    }
}
