using System.Collections.Generic;
using System.Linq;

namespace JenkinsWebApi.Internal
{
    internal static class JsonConverter
    {

        internal static string ToJson(this Dictionary<string, string> dict)
        {
            string str = dict.Select(i => $"\"{i.Key}\": \"{i.Value}\"").Aggregate((a, b) => $"{a}, {b}");
            str = $"{{{str}}}";
            return str;
        }
    }
}
