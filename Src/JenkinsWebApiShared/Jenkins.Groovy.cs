using JenkinsWebApi.Internal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0063 // Use simple 'using' statement
#pragma warning restore IDE0079 // Remove unnecessary suppression

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Run node script
        /// </summary>
        /// <param name="computerName">Name of the computer</param>
        /// <param name="script">Script to run</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunComputerScriptAsync(string computerName, string script)
        {
            return await RunComputerScriptAsync(computerName, script, CancellationToken.None);
        }

        /// <summary>
        /// Run node script
        /// </summary>
        /// <param name="computerName">Name of the computer</param>
        /// <param name="script">Script to run</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunComputerScriptAsync(string computerName, string script, CancellationToken cancellationToken)
        {
            var parms = new Dictionary<string, string>
            {
                { "script", script }
            };
            var content = new FormUrlEncodedContent(parms);
            using (HttpResponseMessage response = await this.client.PostAsync($"computer/{computerName}/script", content, cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                return TrimScript(str);
            }
        }

        /// <summary>
        /// Run node script on master
        /// </summary>
        /// <param name="script">Script to run</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunMasterComputerScriptAsync(string script)
        {
            return await RunMasterComputerScriptAsync(script, CancellationToken.None);
        }

        /// <summary>
        /// Run node script on master
        /// </summary>
        /// <param name="script">Script to run</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunMasterComputerScriptAsync(string script, CancellationToken cancellationToken)
        {
            var parms = new Dictionary<string, string>
            {
                //parms.Add("Jenkins-Crumb", crumb);
                { "script", script }
            };
            var content = new FormUrlEncodedContent(parms);
            using (HttpResponseMessage response = await this.client.PostAsync("computer/(master)/script", content, cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                return TrimScript(str);
            }
        }

        /// <summary>
        /// Get IP of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>IP address</returns>
        public async Task<string> GetComputerHostAddressAsync(string computerName)
        {
            return await GetComputerHostAddressAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get IP of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>IP address</returns>
        public async Task<string> GetComputerHostAddressAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.hostAddress", cancellationToken);
        }

        /// <summary>
        /// Get host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Host name</returns>
        public async Task<string> GetComputerHostNameAsync(string computerName)
        {
            return await GetComputerHostNameAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Host name</returns>
        public async Task<string> GetComputerHostNameAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.hostName", cancellationToken);
        }

        /// <summary>
        /// Get canonical host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Canonical host name</returns>
        public async Task<string> GetComputerCanonicalHostNameAsync(string computerName)
        {
            return await GetComputerCanonicalHostNameAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get canonical host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Canonical host name</returns>
        public async Task<string> GetComputerCanonicalHostNameAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.canonicalHostName", cancellationToken);
        }

        /// <summary>
        /// Calls a shell command
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="command">Command to call</param>
        /// <returns>Command result</returns>
        public async Task<string> GetComputerCommandAsync(string computerName, string command)
        {
            return await GetComputerCommandAsync(computerName, command, CancellationToken.None);
        }

        /// <summary>
        /// Calls a shell command
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="command">Command to call</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Command result</returns>
        public async Task<string> GetComputerCommandAsync(string computerName, string command, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, $"println \"{command}\".execute().text", cancellationToken);
        }

        /// <summary>
        /// Get the client user name
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>User name</returns>
        public async Task<string> GetComputerUserAsync(string computerName)
        {
            return await GetComputerUserAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get the client user name
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>User name</returns>
        public async Task<string> GetComputerUserAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println \"whoami\".execute().text", cancellationToken);
        }
    }
}
