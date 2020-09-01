using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace JenkinsWebApi
{
    /// <summary>
    /// Class for build parameters.
    /// </summary>
    public class JenkinsBuildParameters
    {
        private readonly List<JenkinsParameter> list;

        /// <summary>
        /// Constructor
        /// </summary>
        public JenkinsBuildParameters()
        {
            this.list = new List<JenkinsParameter>();
        }
        
        /// <summary>
        /// Add a string parameter.
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public void Add(string name, string value)
        {
            this.list.Add(new JenkinsParameter() { Type = JenkinsParameterType.String, Name = name, StringValue = value });
        }

        /// <summary>
        /// Add a boolean parameter.
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public void Add(string name, bool value)
        {
            this.list.Add(new JenkinsParameter() { Type = JenkinsParameterType.Boolean, Name = name, BooleanValue = value});
        }

        /// <summary>
        /// Add a stream parameter.
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="stream">Stream of the file to commit</param>
        /// <param name="fileName">Name of the file</param>
        public void Add(string name, Stream stream, string fileName)
        {
            this.list.Add(new JenkinsParameter() { Type = JenkinsParameterType.Stream, Name = name, FileStream = stream, FileName = fileName });
        }

        private HttpContent GetContent()
        {
            if (this.list == null || !this.list.Any())
            {
                return null;
            }
            else if (this.list.Any(p => p.Type == JenkinsParameterType.Stream))
            {
                MultipartFormDataContent content = new MultipartFormDataContent();

                StringBuilder json = new StringBuilder();
                json.Append("{\"parameter\": [");

                int fileIndex = 0;
                foreach (var para in this.list)
                {
                    switch (para.Type)
                    {
                    case JenkinsParameterType.String:
                        content.Add(new StringContent(para.Name), "name");
                        content.Add(new StringContent(para.StringValue), "value");
                        json.Append($"{{\"name\": \"{para.Name}\", \"value\": \"{para.StringValue}\"}}, ");
                        break;

                    case JenkinsParameterType.Stream:
                        content.Add(new StringContent(para.Name), "name");
                        content.Add(new StreamContent(para.FileStream), $"file{fileIndex}", para.FileName);
                        json.Append($"{{\"name\": \"{para.Name}\", \"file\": \"file{fileIndex}\"}}, ");
                        fileIndex++;
                        break;

                    case JenkinsParameterType.Boolean:
                        content.Add(new StringContent(para.Name), "name");                            
                        if (para.BooleanValue)
                        {
                            content.Add(new StringContent(para.StringValue), "value");
                            json.Append($"{{\"name\": \"{para.Name}\", \"value\": true}}, ");
                        }
                        else
                        {
                            json.Append($"{{\"name\": \"{para.Name}\", \"value\": false}}, ");
                        }
                        break;
                    }
                }

                json.Remove(json.Length - 2, 2);
                json.Append($"]}}");
                
                content.Add(new StringContent(json.ToString()), "json");
                content.Add(new StringContent("Build"), "Submit");

                return content;
            }
            else
            {
                List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

                StringBuilder json = new StringBuilder();
                json.Append("{\"parameter\": [");

                foreach (var para in this.list)
                {
                    switch (para.Type)
                    {
                    case JenkinsParameterType.String:
                        param.Add(new KeyValuePair<string, string>("name", para.Name));
                        param.Add(new KeyValuePair<string, string>("value", para.StringValue));
                        json.Append($"{{\"name\": \"{para.Name}\", \"value\": \"{para.StringValue}\"}}, ");

                        break;
                    case JenkinsParameterType.Boolean:
                        param.Add(new KeyValuePair<string, string>("name", para.Name));
                        if (para.BooleanValue)
                        {
                            param.Add(new KeyValuePair<string, string>("value", "on"));
                            json.Append($"{{\"name\": \"{para.Name}\", \"value\": true}}, ");
                        }
                        else
                        {
                            json.Append($"{{\"name\": \"{para.Name}\", \"value\": false}}, ");
                        }
                        break;
                    }
                }
                json.Remove(json.Length - 2, 2);
                json.Append($"]}}");

                string s = json.ToString();

                param.Add(new KeyValuePair<string, string>("json", json.ToString()));
                param.Add(new KeyValuePair<string, string>("Submit", "Build"));

                return new FormUrlEncodedContent(param);
            }
        }

        internal bool IsEmpty { get { return !this.list.Any(); } }

        /// <summary>
        /// Cast JenkinsBuildParameters to HttpContent.
        /// </summary>
        /// <param name="param">JenkinsBuildParameters instance to cast</param>
        public static implicit operator HttpContent(JenkinsBuildParameters param)
        {
            return param.GetContent();
        }

        private class JenkinsParameter
        {
            internal JenkinsParameterType Type { get; set; }

            internal string Name { get; set; }

            internal string StringValue { get; set; }

            internal bool BooleanValue { get; set; }

            internal Stream FileStream { get; set; }

            internal string FileName { get; set; }
        }
    }
}
