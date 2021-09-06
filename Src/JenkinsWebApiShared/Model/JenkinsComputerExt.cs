using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsWebApi.Model
{
    /// <summary>
    /// Class for extendet computer info data.
    /// </summary>
    public class JenkinsComputerExt
    {
        /// <summary>
        /// Description text of a node
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Label list of a node.
        /// </summary>
        public string Label { get; set; }
    }
}
