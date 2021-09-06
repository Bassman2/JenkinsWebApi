using System;

namespace JenkinsWebApi.Internal
{
    internal static class XmlConverter
    {
        public static DateTime? ToDateTime(long? value)
        {
            return value.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(value.Value).ToLocalTime() : null;
        }

        public static TimeSpan? ToTimeSpan(long? value)
        {
            return value.HasValue ? (TimeSpan?)TimeSpan.FromMilliseconds(value.Value) : null;
        }
    }
}


