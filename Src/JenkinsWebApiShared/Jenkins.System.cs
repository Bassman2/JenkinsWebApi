using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Enter into the "quiet down" mode.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task QuiteDownAsync()
        {
            await QuiteDownAsync(CancellationToken.None);
        }

        /// <summary>
        /// Enter into the "quiet down" mode.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task QuiteDownAsync(CancellationToken cancellationToken)
        {
            await PostRunAsync("quietDown", null, cancellationToken);
        }

        /// <summary>
        /// Cancel the "quiet down" mode.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task CancelQuietDown()
        {
            await CancelQuietDown(CancellationToken.None);
        }

        /// <summary>
        /// Cancel the "quiet down" mode.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task CancelQuietDown(CancellationToken cancellationToken)
        {
            await PostRunAsync("cancelQuietDown", null, cancellationToken);
        }

        /// <summary>
        /// Restart the Jenkins Server
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task RestartAsync()
        {
            await RestartAsync(CancellationToken.None);
        }

        /// <summary>
        /// Restart the Jenkins Server
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task RestartAsync(CancellationToken cancellationToken)
        {
            await PostRunAsync("restart", null, cancellationToken);
        }

        /// <summary>
        /// Save restart the Jenkins Server if no job is running
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SaveRestartAsync()
        {
            await SaveRestartAsync(CancellationToken.None);
        }

        /// <summary>
        /// Save restart the Jenkins Server if no job is running
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SaveRestartAsync(CancellationToken cancellationToken)
        {
            await PostRunAsync("safeRestart", null, cancellationToken);
        }

        /// <summary>
        /// Launch slave agent
        /// </summary>
        /// <param name="hostName">Name of the slave host</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task LaunchSlaveAgent(string hostName)
        {
            await LaunchSlaveAgent(hostName, CancellationToken.None);
        }

        /// <summary>
        /// Launch slave agent
        /// </summary>
        /// <param name="hostName">Name of the slave host</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task LaunchSlaveAgent(string hostName, CancellationToken cancellationToken)
        {
            await GetApiStringAsync($"computer/{hostName}/launchSlaveAgent", cancellationToken);
        }
    }
}
