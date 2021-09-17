using System;
using System.Linq;
using System.Net.NetworkInformation;
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

            // convert plugin names to JenkinsWebApi name format
            // Jenkins<manufacturer><plugin><class>

            name = name.ReplaceStart("com.cloudbees.hudson.plugins.folder", "JenkinsCloudbees");
            name = name.ReplaceStart("com.cloudbees.plugins.credentials", "JenkinsCloudbees");
            name = name.ReplaceStart("com.tikal.jenkins.plugins.multijob.views", "JenkinsTikal");
            name = name.ReplaceStart("com.tikal.jenkins.plugins.multijob", "JenkinsTikal");

            name = name.ReplaceStart("org.jenkinsci.plugins.buildgraphview", "JenkinsJenkinsci");
            name = name.ReplaceStart("org.jenkinsci.plugins.categorizedview", "JenkinsJenkinsci");
            name = name.ReplaceStart("org.jenkinsci.plugins.envinject", "JenkinsJenkinsci");
            name = name.ReplaceStart("org.jenkinsci.plugins.workflow.job", "JenkinsJenkinsci");
            name = name.ReplaceStart("org.jenkinsci.plugins.workflow.multibranch", "JenkinsJenkinsci");

            // replace old product name with new
            name = name.ReplaceStart("hudson", "Jenkins");

            name = name.HungarianNotation();             
            name = name.StartsWith("Jenkins") ? name : "Jenkins" + name;
            return name;
        }


        public static string JenkinsToItemName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            //name = name.Replace("-", "");
            //if (name.Contains('_') || name.Contains('.'))
            //{
            //    return name.Split('_', '.').Cast<string>().Select(s => s.UpperFirstChar()).Aggregate((a, b) => a + b);
            //}
            //return name.UpperFirstChar();
            return name.HungarianNotation();
        }

        public static string JenkinsToEnumItemName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return name.ToLower().HungarianNotation();
        }

        public static string HungarianNotation(this string str)
        {
            _ = str ?? throw new ArgumentNullException(nameof(str));

            //name = name.Replace("-", "");
            if (str.Contains('_') || str.Contains('-') || str.Contains('.'))
            {
                str = str.Split('_', '.', '-').Cast<string>().Select(s => s.UpperFirstChar()).Aggregate((a, b) => a + b);
            }
            return str.UpperFirstChar();
        }

        private static string UpperFirstChar(this string str)
        {
            _ = str ?? throw new ArgumentNullException(nameof(str));

            StringBuilder sb = new StringBuilder(str);
            sb[0] = char.ToUpper(sb[0]);
            return sb.ToString();
        }

        private static string ReplaceStart(this string str, string find, string replace)
        {
            _ = str ?? throw new ArgumentNullException(nameof(str));
            _ = find ?? throw new ArgumentNullException(nameof(find));

            if (str.StartsWith(find))
            {
                str = (replace ?? string.Empty) + str.Substring(find.Length);
            }
            return str;
        }
    }
}
