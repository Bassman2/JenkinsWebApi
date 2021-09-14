using JenkinsWebApi.Model;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        /// <remarks>For compatibility to old Jenkins version. For new version use GetAsyncPeopleAsync instead.</remarks>
        public async Task<JenkinsModelViewPeople> GetPeopleAsync()
        {
            return await GetPeopleAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List of all Jenkins users</returns>
        /// <remarks>For compatibility to old Jenkins version. For new version use GetAsyncPeopleAsync instead.</remarks>
        public async Task<JenkinsModelViewPeople> GetPeopleAsync(CancellationToken cancellationToken)
        {
            JenkinsModelViewPeople people = await GetAsync<JenkinsModelViewPeople>("people/api/xml", cancellationToken);
            return people;
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        public async Task<JenkinsModelViewAsynchPeoplePeople> GetAsyncPeopleAsync()
        {
            return await GetAsyncPeopleAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List of all Jenkins users</returns>
        public async Task<JenkinsModelViewAsynchPeoplePeople> GetAsyncPeopleAsync(CancellationToken cancellationToken)
        {
            JenkinsModelViewAsynchPeoplePeople people = await GetAsync<JenkinsModelViewAsynchPeoplePeople>("asynchPeople/api/xml", cancellationToken);
            return people;
        }

        /// <summary>
        /// Get the data of one Jenkins user.
        /// </summary>
        /// <param name="userName">Name of the Jenkins user</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetUserAsync(string userName)
        {
            return await GetUserAsync(userName, CancellationToken.None);
        }

        /// <summary>
        /// Get the data of one Jenkins user.
        /// </summary>
        /// <param name="userName">Name of the Jenkins user</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetUserAsync(string userName, CancellationToken cancellationToken)
        {
            JenkinsModelUser user = await GetAsync<JenkinsModelUser>($"user/{userName}/api/xml", cancellationToken);
            return user;
        }

        /// <summary>
        /// Get the data of the current login user.
        /// </summary>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetCurrentUserAsync()
        {
            return await GetCurrentUserAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the data of the current login user.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            JenkinsModelUser user = await GetAsync<JenkinsModelUser>("me/api/xml", cancellationToken);
            return user;
        }
    }
}
