namespace JenkinsWebApi;

/// <summary>
/// Status of a run
/// </summary>
public enum RunStatus
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
    /// The build is stucked.
    /// </summary>
    /// <remarks>
    /// This can occure if the nodes are offline.<br/>
    /// See JenkinsRunProgress
    /// </remarks>
    Stuck,
    /// <summary>
    /// The build is blocked.
    /// </summary>
    Blocked,

    /// <summary>
    /// The Jenkins job is disabled.
    /// </summary>
    Disabled,

    /// <summary>
    /// The build has been canceled.
    /// </summary>
    Canceled

}
