using System;
using System.Linq;

namespace FutureArbitrage.Util
{
    public static class EnumHelper
    {
        public static T[] GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).OfType<T>().ToArray();
        }
    }
}
