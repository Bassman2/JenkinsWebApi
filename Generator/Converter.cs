using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public static class Converter
    {
        public static string JenkinsToClassName(string name)
        {
            if (name.StartsWith("hudson."))
            {
                name = name.Substring(7);
            }
            if (name.StartsWith("jenkins."))
            {
                name = name.Substring(8);
            }

            //StringBuilder sb = new StringBuilder(name);

            //sb.Replace(".a", "A");
            //sb.Replace(".b", "B");
            //sb.Replace(".c", "C");
            //sb.Replace(".d", "D");
            //sb.Replace(".e", "E");
            //sb.Replace(".f", "F");
            //sb.Replace(".g", "G");
            //sb.Replace(".h", "H");
            //sb.Replace(".i", "I");
            //sb.Replace(".j", "J");
            //sb.Replace(".k", "K");
            //sb.Replace(".l", "L");
            //sb.Replace(".m", "M");
            //sb.Replace(".n", "N");
            //sb.Replace(".o", "O");
            //sb.Replace(".p", "P");
            //sb.Replace(".q", "Q");
            //sb.Replace(".r", "R");
            //sb.Replace(".s", "S");
            //sb.Replace(".t", "T");
            //sb.Replace(".u", "U");
            //sb.Replace(".v", "V");
            //sb.Replace(".w", "W");
            //sb.Replace(".x", "X");
            //sb.Replace(".y", "Y");
            //sb.Replace(".z", "Z");
            //sb.Replace(".", "");
            //sb.Replace("-", "");

            //sb[0] = char.ToUpper(sb[0]);

            //return "Jenkins" + sb.ToString();
            return "Jenkins" + JenkinsToItemName(name);
        }


        public static string JenkinsToItemName(string str)
        {
            str = str.Replace("-", "");
            if (str.Contains('_') || str.Contains('.'))
            {
                return str.Split('_', '.').Cast<string>().Select(s => UpperFirstChar(s.ToLower())).Aggregate((a, b) => a + b);
            }
            return UpperFirstChar(str.ToLower());
        }

        private static string UpperFirstChar(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb[0] = char.ToUpper(sb[0]);
            return sb.ToString();
        }
    }
}
