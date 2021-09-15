using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsWebApi
{
    /// <summary>
    /// Status of a run
    /// </summary>
    public enum JenkinsRunStatus
    {
        /// <summary>
        /// The build is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The build is building.
        /// </summary>
        Building,

        /// <summary>
        /// The build has finished.
        /// </summary>
        Finished,

        /// <summary>
        /// The build is blocked.
        /// </summary>
        Blocked,

        /// <summary>
        /// The build has been canceled.
        /// </summary>
        Canceled

    }
}
