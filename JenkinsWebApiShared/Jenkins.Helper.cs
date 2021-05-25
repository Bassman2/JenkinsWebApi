using HtmlAgilityPack;
using JenkinsWebApi.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Net.Http.Json;
using System.Text.Json;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        private async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken) where T : class
        {
            T value = null;
            using (HttpResponseMessage response = await this.client.GetAsync(path, cancellationToken))
            {
                response.EnsureSuccess();
                //string str = await response.Content.ReadAsStringAsync();
                //XmlSerializer serializer = new XmlSerializer(typeof(T));
                //value = (T)serializer.Deserialize(new StringReader(str));
                value = await response.Content.ReadFromJsonAsync<T>((JsonSerializerOptions)null, cancellationToken);
            }
            return value;
        }

        private async Task<string> GetStringAsync(string path, CancellationToken cancellationToken)
        {
            string value = null;
            using (HttpResponseMessage response = await this.client.GetAsync(path, cancellationToken))
            {
                response.EnsureSuccess();
                value = await response.Content.ReadAsStringAsync();
            }
            return value;
        }

        private async Task<bool> PostLoginAsync(string path, HttpContent content, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                response.EnsureSuccess();
            }
            return true;
        }

        private async Task<Uri> PostRunAsync(string path, HttpContent content, CancellationToken cancellationToken)
        {
            Uri location = null;
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
            {
                response.EnsureSuccess();
                location = response.Headers.Location;
            }

            return location;
        }

        private T Deserialize<T>(string xmlText, IEnumerable<Type> classTypes)
        {
            using (XmlTextReader reader = new XmlTextReader(new StringReader(xmlText)))
            {
                foreach (Type t in classTypes)
                {
                    XmlSerializer serializer = new XmlSerializer(t);
                    if (serializer.CanDeserialize(reader))
                    {
                        return (T)serializer.Deserialize(reader);
                    }
                }
            }
            throw new Exception($"Not class found for this type: {xmlText.Substring(1, xmlText.IndexOf(' '))}");
        }

        private string TrimScript(string str)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            var el = doc.DocumentNode.SelectSingleNode("//*[@id='main-panel']/pre[last()]");        // Jenkins ver. 1.424.6
            if (el != null)
            {
                return HtmlEntity.DeEntitize(el.InnerText).Trim();
            }
            return null;
        }

        private string TrimDescription(string str)
        {
            str = str.Replace("&&", "");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            var el = doc.DocumentNode.SelectSingleNode("//input[@name='_.nodeDescription']");        // Jenkins ver. 1.424.6
            if (el != null)
            {
                HtmlAttribute attr = el.Attributes["value"];
                string res = HtmlEntity.DeEntitize(attr.Value);
                return res.Trim();
            }
            return null;
        }

        private string TrimLabel(string str)
        {
            str = str.Replace("&&", "");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            var el = doc.DocumentNode.SelectSingleNode("//input[@name='_.labelString']");        // Jenkins ver. 1.424.6
            if (el != null)
            {
                HtmlAttribute attr = el.Attributes["value"];
                string res = HtmlEntity.DeEntitize(attr.Value);
                return res.Trim();
            }
            return null;
        }
    }
}
