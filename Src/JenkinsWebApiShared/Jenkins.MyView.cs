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

            JenkinsModelView view = await GetApiViewAsync<JenkinsModelView>($"me/my-views/view/{viewName}", cancellationToken);
            return view;
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view.</param>
        /// <returns>Returns view data.</returns>
        public async Task<T> GetMyViewAsync<T>(string viewName) where T : JenkinsModelView
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
        public async Task<T> GetMyViewAsync<T>(string viewName, CancellationToken cancellationToken) where T : JenkinsModelView
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            T view = await GetApiViewAsync<T>($"me/my-views/view/{viewName}", cancellationToken);
            return view;
        }
    }
}
