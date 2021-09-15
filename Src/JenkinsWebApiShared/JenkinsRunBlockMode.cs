using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsWebApi
{
    /// <summary>
    /// Select what to do if a jekins build blocks.
    /// </summary>
    public enum JenkinsRunBlockMode
    {
        /// <summary>
        /// Poll until the blocking reason is removed.
        /// </summary>
        Continue,

        /// <summary>
        /// Leave the method on blocking
        /// </summary>
        Leave,

        /// <summary>
        /// Throw an Exception if the build blocks.
        /// </summary>
        Exception
    }
}
