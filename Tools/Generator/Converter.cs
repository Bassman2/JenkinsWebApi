using System;
using System.Linq;
using System.Text;

namespace Generator
{
    public static class Converter
    {
        public static string JenkinsToClassName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.StartsWith("hudson."))
            {
                name = name.Substring(7);
            }
            if (name.StartsWith("jenkins."))
            {
                name = name.Substring(8);
            }

            return "Jenkins" + JenkinsToItemName(name);
        }


        public static string JenkinsToItemName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = name.Replace("-", "");
            if (name.Contains('_') || name.Contains('.'))
            {
                return name.Split('_', '.').Cast<string>().Select(s => UpperFirstChar(s)).Aggregate((a, b) => a + b);
            }
            return UpperFirstChar(name);
        }

        public static string JenkinsToEnumItemName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return JenkinsToItemName(name.ToLower());
        }

        private static string UpperFirstChar(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb[0] = char.ToUpper(sb[0]);
            return sb.ToString();
        }
    }
}
