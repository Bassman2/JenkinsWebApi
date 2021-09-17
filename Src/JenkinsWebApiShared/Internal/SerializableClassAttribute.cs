using System;

namespace JenkinsWebApi.Internal
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class SerializableClassAttribute : Attribute
    {
        public string ClassName { get; }
        
        public SerializableClassAttribute(string className)
        {
            this.ClassName = className;
        }

        public static string GetClassName(Type t)
        {
            SerializableClassAttribute attr = Attribute.GetCustomAttribute(t, typeof(SerializableClassAttribute)) as SerializableClassAttribute;
            return attr.ClassName;
        }
    }
}
