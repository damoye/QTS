using System;

namespace FutureArbitrage.Contract
{
    public class Log
    {
        public Log(string content)
        {
            this.Content = content;
            this.Time = DateTime.Now;
        }

        public string Content { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return this.Time.ToString() + " " + this.Content;
        }
    }
}