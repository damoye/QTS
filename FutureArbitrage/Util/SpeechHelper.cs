using FutureArbitrage.BLL;
using System;
using System.Speech.Synthesis;

namespace FutureArbitrage.Util
{
    public static class SpeechHelper
    {
        private static SpeechSynthesizer speecher = new SpeechSynthesizer();

        public static void Speak(string content)
        {
            try
            {
                speecher.Speak(content);
            }
            catch (Exception e)
            {
                LogCenter.Error(e.Message);
            }
        }
    }
}