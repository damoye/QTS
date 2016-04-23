using System;

namespace FutureArbitrage.Contract
{
    public class Order
    {
        public string InstrumentID { get; set; }
        public string UserID { get; set; }
        public DirectionType Direction { get; set; }
        public OffsetFlag CombOffsetFlag { get; set; }
        public double LimitPrice { get; set; }
        public int VolumeTotalOriginal { get; set; }
        public OrderSubmitStatus OrderSubmitStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int VolumeTraded { get; set; }
        public int VolumeTotal { get; set; }
        public DateTime InsertTime { get; set; }
        public string StatusMsg { get; set; }
    }
}