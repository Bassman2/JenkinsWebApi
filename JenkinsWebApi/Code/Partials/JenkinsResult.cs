using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public enum JenkinsResult
    {
        /// <summary>
        /// Build was successfully completed.
        /// </summary>
        [XmlEnum("SUCCESS")]
        Success,

        /// <summary>
        /// Build has ended in an unstable state.
        /// </summary>
        [XmlEnum("UNSTABLE")]
        Unstable,

        /// <summary>
        /// Build has ended with at least one error.
        /// </summary>
        [XmlEnum("FAILURE")]
        Failure
    }
}
