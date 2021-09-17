using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Internal
{
    internal static class JenkinsDeserializer
    {
        public const string ApiFormat = "/api/xml";

        private readonly static Type[] viewTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(t => typeof(JenkinsModelView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                .ToArray();

        private readonly static Type[] jobTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelJob).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private readonly static Type[] buildTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelRun).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        public static T Deserialize<T>(string text) 
        {
            return JsonSerializer.Deserialize<T>(text);
        }

        public static T DeserializeView<T>(string text)
        {
            return Deserialize<T>(text, viewTypes);
        }

        public static T DeserializeJob<T>(string text)
        {
            return Deserialize<T>(text, jobTypes);
        }

        public static T DeserializeBuild<T>(string text)
        {
            return Deserialize<T>(text, buildTypes);
        }

        private static T Deserialize<T>(string text, IEnumerable<Type> classTypes) 
        {
            _ = classTypes;
            return Deserialize<T>(text); 

            //using (XmlTextReader reader = new XmlTextReader(new StringReader(xmlText)))
            //{
            //    foreach (Type t in classTypes)
            //    {
            //        XmlSerializer serializer = new XmlSerializer(t);
            //        if (serializer.CanDeserialize(reader))
            //        {
            //            return (T)serializer.Deserialize(reader);
            //        }
            //    }
            //}
            //throw new Exception($"Not class found for this type: {xmlText.Substring(1, xmlText.IndexOf(' '))}");
        }
    }
}
