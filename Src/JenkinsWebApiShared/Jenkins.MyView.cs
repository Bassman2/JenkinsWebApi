using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <returns>Returns view data.</returns>
        public async Task<JenkinsModelView> GetMyViewAsync(string viewName)
        {
            return await GetMyViewAsync(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns view data.</returns>
        public async Task<JenkinsModelView> GetMyViewAsync(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetApiStringAsync($"me/my-views/view/{viewName}", cancellationToken);
            JenkinsModelView view = JenkinsDeserializer.DeserializeView<JenkinsModelView>(str);
            return view;
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view.</param>
        /// <returns>Returns view data.</returns>
        public async Task<T> GetMyViewAsync<T>(string viewName) where T : class
        {
            return await GetMyViewAsync<T>(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns view data.</returns>
        public async Task<T> GetMyViewAsync<T>(string viewName, CancellationToken cancellationToken) where T : class
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetApiStringAsync($"me/my-views/view/{viewName}", cancellationToken);
            var view = JenkinsDeserializer.DeserializeView<T>(str);
            return view;
        }
    }
}
