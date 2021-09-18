using JenkinsWebApi.Model;
using JenkinsWebApi.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JenkinsWebApi.Internal
{
    internal static class JenkinsDeserializer
    {
        public const string ApiFormat = "/api/xml";

        private readonly static Dictionary<string, Type> viewTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(JenkinsModelView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
            .ToDictionary(t => SerializableClassAttribute.GetClassName(t), t => t);

        private readonly static Dictionary<string, Type> jobTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(JenkinsModelJob).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
            .ToDictionary(t => SerializableClassAttribute.GetClassName(t), t => t);

        private readonly static Dictionary<string, Type> buildTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(JenkinsModelRun).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
            .ToDictionary(t => SerializableClassAttribute.GetClassName(t), t => t);

        public static T Deserialize<T>(string text) where T : class
        {
            return new XmlSerializer(typeof(T)).Deserialize(new StringReader(text)) as T;
        }

        public static T DeserializeView<T>(string text) where T : JenkinsModelView
        {
            return Deserialize<T>(text, viewTypes);
        }

        public static T DeserializeJob<T>(string text) where T : JenkinsModelJob
        {
            return Deserialize<T>(text, jobTypes);
        }

        public static T DeserializeBuild<T>(string text) where T : JenkinsModelRun
        {
            return Deserialize<T>(text, buildTypes);
        }

        private static T Deserialize<T>(string text, Dictionary<string, Type> classTypes) where T : class
        {

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(text);
            var typeValue = xmlDocument.DocumentElement.GetAttribute("_class");

            if (classTypes.TryGetValue(typeValue, out Type type))
            {
                return new XmlSerializer(type).Deserialize(new StringReader(text)) as T;
            }
            
            return default;
        }
    }
}
