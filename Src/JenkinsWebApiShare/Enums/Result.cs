namespace JenkinsWebApi;

/// <summary>
/// Jenkins Result
/// </summary>
public enum JenkinsResult
{
    /// <summary>
    /// Success
    /// </summary>
    [XmlEnum("SUCCESS")]
    Success,

    /// <summary>
    /// Failure
    /// </summary>
    [XmlEnum("FAILURE")]
    Failure
}