using JenkinsWebApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <returns> Returns view data.</returns>
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
        public async Task<JenkinsModelView> GetViewAsync(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetStringAsync($"view/{viewName}/api/xml", cancellationToken);
            JenkinsModelView view = Deserialize<JenkinsModelView>(str, viewTypes);
            return view;
        }

        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view.</param>
        /// <returns> Returns view data.</returns>
        /// <remarks>
        /// <list type="table">
        /// <listheader>
        /// <term>View</term>
        /// <term>Class</term>
        /// <term>Plugin</term>
        /// </listheader>
        /// <item>
        /// <term>List View</term>
        /// <term><see cref="JenkinsModelListView"/></term>
        /// <term>-</term>
        /// </item>
        /// <item>
        /// <term>My View</term>
        /// <term><see cref="JenkinsModelMyView"/></term>
        /// <term>-</term>
        /// </item>
        /// <item>
        /// <term>All View</term>
        /// <term><see cref="JenkinsModelAllView"/></term>
        /// <term>-</term>
        /// </item>
        /// <item>
        /// <term>Categorized Jobs View</term>
        /// <term><see cref="JenkinsJenkinsciCategorizedJobsView"/></term>
        /// <term><see href="https://plugins.jenkins.io/categorized-view/">categorized-view</see></term>
        /// </item>
        /// <item>
        /// <term>Dashboard</term>
        /// <term><see cref="JenkinsPluginsViewDashboardDashboard"/></term>
        /// <term><see href="https://plugins.jenkins.io/dashboard-view/">Dashboard View</see></term>
        /// </item>
        /// <item>
        /// <term>Multijob View</term>
        /// <term><see cref="JenkinsTikalMultiJobView"/></term>
        /// <term><see href="https://plugins.jenkins.io/jenkins-multijob-plugin/">Multijob</see></term>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<T> GetViewAsync<T>(string viewName)
        {
            return await GetViewAsync<T>(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns view data.</returns>
        public async Task<T> GetViewAsync<T>(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetStringAsync($"view/{viewName}/api/xml", cancellationToken);
            var view = Deserialize<T>(str, viewTypes);
            return view;
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <returns> Returns view data.</returns>
        public async Task<JenkinsModelView> GetMyViewAsync(string viewName)
        {
            return await GetMyViewAsync(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns view data.</returns>
        public async Task<JenkinsModelView> GetMyViewAsync(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetStringAsync($"me/my-views/view/{viewName}/api/xml", cancellationToken);
            JenkinsModelView view = Deserialize<JenkinsModelView>(str, viewTypes);
            return view;
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view.</param>
        /// <returns> Returns view data.</returns>
        public async Task<T> GetMyViewAsync<T>(string viewName)
        {
            return await GetMyViewAsync<T>(viewName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins my view data.
        /// </summary>
        /// <typeparam name="T">Type of view return data.</typeparam> 
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns view data.</returns>
        public async Task<T> GetMyViewAsync<T>(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetStringAsync($"me/my-views/view/{viewName}/api/xml", cancellationToken);
            var view = Deserialize<T>(str, viewTypes);
            return view;
        }
    }
}
