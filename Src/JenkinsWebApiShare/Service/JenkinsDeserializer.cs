
namespace JenkinsWebApi.Service;


internal static class JenkinsDeserializer
{
    public const string ApiFormat = "/api/xml";

    //private readonly static Dictionary<string, Type> viewTypes = AppDomain.CurrentDomain.GetAssemblies()
    //    .SelectMany(static s => s.GetTypes())
    //    .Where(t => typeof(JenkinsModelView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
    //    .ToDictionary(t => SerializableClassAttribute.GetClassName(t), t => t);

    //// for "Folder" job use JenkinsModelAbstractItem instead of JenkinsModelJob
    //private readonly static Dictionary<string, Type> jobTypes = AppDomain.CurrentDomain.GetAssemblies()
    //    .SelectMany(s => s.GetTypes())
    //    .Where(t => typeof(JenkinsModelAbstractItem).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
    //    .ToDictionary(t => SerializableClassAttribute.GetClassName(t), t => t);

    //private readonly static Dictionary<string, Type> buildTypes = AppDomain.CurrentDomain.GetAssemblies()
    //    .SelectMany(s => s.GetTypes())
    //    .Where(t => typeof(JenkinsModelRun).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
    //    .ToDictionary(t => SerializableClassAttribute.GetClassName(t), t => t);

    //private readonly static Dictionary<string, Type> queueTypes = new Dictionary<string, Type>()
    //    {
    //        { "hudson.model.Queue-LeftItem", typeof(JenkinsModelQueueLeftItem) },
    //        { "hudson.model.Queue-BuildableItem", typeof(JenkinsModelQueueBuildableItem) },
    //        { "hudson.model.Queue-BlockedItem", typeof(JenkinsModelQueueBlockedItem) }

    //    };

    [RequiresUnreferencedCode("Calls System.Xml.Serialization.XmlSerializer.XmlSerializer(Type)")]
    public static T? Deserialize<T>(string text) where T : class
    {
        return (T?)(new XmlSerializer(typeof(T)).Deserialize(new StringReader(text)));
    }

    //public static T DeserializeView<T>(string text) where T : JenkinsModelView
    //{
    //    return Deserialize<T>(text, viewTypes);
    //}

    //public static T DeserializeJob<T>(string text) where T : JenkinsModelAbstractItem
    //{
    //    return Deserialize<T>(text, jobTypes);
    //}

    //public static T DeserializeBuild<T>(string text) where T : JenkinsModelRun
    //{
    //    return Deserialize<T>(text, buildTypes);
    //}

    //private static T Deserialize<T>(string text, Dictionary<string, Type> classTypes) where T : class
    //{
    //    var xmlDocument = new XmlDocument();
    //    xmlDocument.LoadXml(text);
    //    var typeValue = xmlDocument.DocumentElement!.GetAttribute("_class");

    //    if (classTypes.TryGetValue(typeValue, out Type type))
    //    {
    //        return new XmlSerializer(type).Deserialize(new StringReader(text)) as T;
    //    }

    //    return default;
    //}

    //public static async Task<T?> ReadAsAsync<T>(this HttpContent content, CancellationToken cancellationToken) where T : class
    //{
    //    string str = await content.ReadAsStringAsync(cancellationToken);
    //    var value = new XmlSerializer(typeof(T)).Deserialize(new StringReader(str)) as T;
    //    return value;
    //}

    //public static async Task<T?> ReadAsViewAsync<T>(this HttpContent content, CancellationToken cancellationToken) where T : JenkinsModelView
    //{
    //    string str = await content.ReadAsStringAsync(cancellationToken);
    //    var value = Deserialize<T>(str, viewTypes);
    //    return value;
    //}

    //public static async Task<T?> ReadAsJobAsync<T>(this HttpContent content, CancellationToken cancellationToken) where T : JenkinsModelAbstractItem
    //{
    //    string str = await content.ReadAsStringAsync(cancellationToken); 
    //    var value = Deserialize<?T>(str, jobTypes);
    //    return value;
    //}

    //public static async Task<T> ReadAsBuildAsync<T>(this HttpContent content, CancellationToken cancellationToken) where T : JenkinsModelRun
    //{
    //    string str = await content.ReadAsStringAsync(cancellationToken);
    //    var value = Deserialize<T>(str, buildTypes);
    //    return value;
    //}
}