using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public partial class JenkinsJenkinsciEnvInjectVarList
    {
        /// <summary>
        /// Environment variable list as dictionary.
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, string> EnvMapDict { get { return (EnvMap as XmlNode[])?.ToDictionary(n => n.Name, n => n.InnerText); } }
    }
}
