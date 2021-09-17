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
        public const string ApiFormat = "/api/json";

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
            return JsonSerializer.Deserialize<T>(text);
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
            var jsonDocument = JsonDocument.Parse(text);
            var typeValue = jsonDocument.RootElement.GetProperty("_class").GetString();

            if (classTypes.TryGetValue(typeValue, out Type type))
            {
                return (T)JsonSerializer.Deserialize(text, type);
            }
            
            return default(T);
        }
    }
}
