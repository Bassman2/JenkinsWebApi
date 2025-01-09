namespace JenkinsWebApi;

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
        this.list.Add(new JenkinsParameter(name, value));
    }

    /// <summary>
    /// Add a boolean parameter.
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    public void Add(string name, bool value)
    {
        this.list.Add(new JenkinsParameter(name, value));
    }

    /// <summary>
    /// Add a stream parameter.
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="stream">Stream of the file to commit</param>
    /// <param name="fileName">Name of the file</param>
    public void Add(string name, Stream stream, string fileName)
    {
        this.list.Add(new JenkinsParameter(name, stream, fileName));
    }

    private HttpContent? GetContent()
    {
        if (this.list == null || !this.list.Any())
        {
            return null;
        }
        else if (this.list.Any(p => p.Type == JenkinsParameterType.Stream))
        {
            // multipart/form-data

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
                    content.Add(new StringContent(para.StringValue!), "value");
                    json.Append($"{{\"name\": \"{para.Name}\", \"value\": \"{para.StringValue}\"}}, ");
                    break;

                case JenkinsParameterType.Stream:
                    content.Add(new StringContent(para.Name), "name");
                    content.Add(new StreamContent(para.FileStream!), $"file{fileIndex}", para.FileName!);
                    json.Append($"{{\"name\": \"{para.Name}\", \"file\": \"file{fileIndex}\"}}, ");
                    fileIndex++;
                    break;

                case JenkinsParameterType.Boolean:
                    content.Add(new StringContent(para.Name), "name");
                    if (para.BooleanValue)
                    {
                        content.Add(new StringContent("on"), "value");
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
            // application/x-www-form-urlencoded

            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

            StringBuilder json = new StringBuilder();
            json.Append("{\"parameter\": [");

            foreach (var para in this.list)
            {
                switch (para.Type)
                {
                case JenkinsParameterType.String:
                    param.Add(new KeyValuePair<string, string>("name", para.Name));
                    param.Add(new KeyValuePair<string, string>("value", para.StringValue!));
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

            // application/x-www-form-urlencoded
            return new FormUrlEncodedContent(param);
        }
    }

    /// <summary>
    /// Cast JenkinsBuildParameters to HttpContent.
    /// </summary>
    /// <param name="param">JenkinsBuildParameters instance to cast</param>
    /// <returns>Cast to HttpContent.</returns>
    public static implicit operator HttpContent?(JenkinsBuildParameters param)
    {
        return param?.GetContent();
    }

    private class JenkinsParameter
    {
        public JenkinsParameter(string name, string value) 
        {
            this.Type = JenkinsParameterType.String;
            this.Name = name;
            this.StringValue = value;
        }

        public JenkinsParameter(string name, bool value)
        {
            this.Type = JenkinsParameterType.Boolean;
            this.Name = name;
            this.BooleanValue = value;
        }

        public JenkinsParameter(string name, Stream stream, string fileName) 
        {
            this.Type = JenkinsParameterType.Stream;
            this.Name = name;
            this.FileStream = stream;
            this.FileName = fileName; 
        }

        internal JenkinsParameterType Type { get;  }

        internal string Name { get;  }

        internal string? StringValue { get; }

        internal bool BooleanValue { get; }

        internal Stream? FileStream { get; }

        internal string? FileName { get; }
    }

    /// <summary>
    /// Type of the Jenkins start job parameter
    /// </summary>
    private enum JenkinsParameterType
    {
        /// <summary>
        /// The parameter is a string.
        /// </summary>
        String,

        /// <summary>
        /// The parameter is a boolean.
        /// </summary>
        Boolean,

        /// <summary>
        /// The parameter is a file stream.
        /// </summary>
        Stream
    }
}
