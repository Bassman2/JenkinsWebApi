namespace JenkinsWebApi;

/// <summary>
/// Class for build parameters.
/// </summary>
public class JenkinsBuildParameters
{
    private readonly List<Parameter> list = [];

    /// <summary>
    /// Add a string parameter.
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    public void Add(string name, string value) => this.list.Add(new Parameter(name, value));

    /// <summary>
    /// Add a boolean parameter.
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    public void Add(string name, bool value) => this.list.Add(new Parameter(name, value));

    /// <summary>
    /// Add a stream parameter.
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="stream">Stream of the file to commit</param>
    /// <param name="fileName">Name of the file</param>
    public void Add(string name, Stream stream, string fileName) => this.list.Add(new Parameter(name, stream, fileName));

    private HttpContent? GetContent()
    {
        if (this.list.Count == 0)
        {
            return null;
        }

        // create JSON parameter
        int fileIndex = 0;
        string values = this.list.Select(p => p.JsonText(ref fileIndex)).Aggregate("", (a,b) => $"{a},{b}");
        string jsonParams = $"{{\"parameter\": [{values}]}}";

        if (this.list.Any(p => p.Type == ParameterType.Stream))
        {
            // multipart/form-data

            var content = new MultipartFormDataContent();

            var json = new StringBuilder();
            json.Append("{\"parameter\": [");

            fileIndex = 0;
            foreach (var para in this.list)
            {
                switch (para.Type)
                {
                case ParameterType.String:
                    content.Add(new StringContent(para.Name), "name");
                    content.Add(new StringContent(para.Value!), "value");
                    json.Append($"{{\"name\": \"{para.Name}\", \"value\": \"{para.Value}\"}}, ");
                    break;

                case ParameterType.Stream:
                    content.Add(new StringContent(para.Name), "name");
                    content.Add(new StreamContent(para.FileStream!), $"file{fileIndex}", para.Value!);
                    json.Append($"{{\"name\": \"{para.Name}\", \"file\": \"file{fileIndex}\"}}, ");
                    fileIndex++;
                    break;

                case ParameterType.Boolean:
                    content.Add(new StringContent(para.Name), "name");
                    if (para.Flag)
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

            var param = new List<KeyValuePair<string, string>>();

            var json = new StringBuilder();
            json.Append("{\"parameter\": [");

            foreach (var para in this.list)
            {
                switch (para.Type)
                {
                case ParameterType.String:
                    param.Add(new KeyValuePair<string, string>("name", para.Name));
                    param.Add(new KeyValuePair<string, string>("value", para.Value!));
                    json.Append($"{{\"name\": \"{para.Name}\", \"value\": \"{para.Value}\"}}, ");

                    break;
                case ParameterType.Boolean:
                    param.Add(new KeyValuePair<string, string>("name", para.Name));
                    if (para.Flag)
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
        return param.GetContent();
    }

    private class Parameter
    {
        public Parameter(string name, string value)
        {
            this.Type = ParameterType.String;
            this.Name = name;
            this.Value = value;
        }

        public Parameter(string name, bool value)
        {
            this.Type = ParameterType.Boolean;
            this.Name = name;
            this.Value = value ? "true" : "false";  // for JSON string
            this.Flag = value;
        }

        public Parameter(string name, Stream stream, string fileName)
        {
            this.Type = ParameterType.Stream;
            this.Name = name;
            this.Value = fileName;
            this.FileStream = stream;
        }

        internal ParameterType Type { get; }

        internal string Name { get; }

        internal string Value { get; }

        internal bool Flag { get; }

        internal Stream? FileStream { get; }

        internal string JsonText(ref int fileIndex)
        {
            return Type switch
            {
                ParameterType.String =>  $"{{\"name\": \"{Name}\", \"value\": \"{Value}\"}}",
                ParameterType.Boolean => $"{{\"name\": \"{Name}\", \"value\": {Value}}}",
                ParameterType.Stream =>  $"{{\"name\": \"{Name}\", \"file\": \"file{fileIndex++}\"}}",
                _ => throw new NotSupportedException()
            };
        }
    }


    /// <summary>
    /// Type of the Jenkins start job parameter
    /// </summary>
    internal enum ParameterType
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
