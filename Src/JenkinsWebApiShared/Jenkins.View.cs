using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <returns> Returns view data.</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='view']/*"/></remarks>
        public async Task<JenkinsModelView> GetViewAsync(string viewName)
        {
            return await GetViewAsync(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns view data.</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='view']/*"/></remarks>
        public async Task<JenkinsModelView> GetViewAsync(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetApiStringAsync($"view/{viewName}", cancellationToken);
            JenkinsModelView view = JenkinsDeserializer.DeserializeView<JenkinsModelView>(str);
            return view;
        }

        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view.</param>
        /// <returns>Returns view data.</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='view']/*"/></remarks>
        public async Task<T> GetViewAsync<T>(string viewName) where T : JenkinsModelView
        {
            return await GetViewAsync<T>(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns view data.</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='view']/*"/></remarks>
        public async Task<T> GetViewAsync<T>(string viewName, CancellationToken cancellationToken) where T : JenkinsModelView
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetApiStringAsync($"view/{viewName}", cancellationToken);
            var view = JenkinsDeserializer.DeserializeView<T>(str);
            return view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public async Task<XmlDocument> GetViewConfigXmlAsync(string viewName)
        {
            return await GetViewConfigXmlAsync(viewName, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<XmlDocument> GetViewConfigXmlAsync(string viewName, CancellationToken cancellationToken)
        {
            string str = await GetViewConfigAsync(viewName, cancellationToken);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public async Task<string> GetViewConfigAsync(string viewName)
        {
            return await GetViewConfigAsync(viewName, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> GetViewConfigAsync(string viewName, CancellationToken cancellationToken)
        {
            string str = await GetStringAsync($"/view/{viewName}/config.xml", cancellationToken);
            return str;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetViewConfigXmlAsync(string viewName, XmlDocument config)
        {
            await SetViewConfigXmlAsync(viewName, config, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="config"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SetViewConfigXmlAsync(string viewName, XmlDocument config, CancellationToken cancellationToken)
        {
            await SetViewConfigAsync(viewName, config.ToString(), cancellationToken); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetViewConfigAsync(string viewName, string config)
        {
            await SetViewConfigAsync(viewName, config, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="config"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SetViewConfigAsync(string viewName, string config, CancellationToken cancellationToken)
        {
            await PostAsync($"/view/{viewName}/config.xml", cancellationToken);

        }

    }
}
