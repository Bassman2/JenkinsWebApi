namespace JenkinsWebApi.Model;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public class PostRunRes
{
    /// <summary>
    /// 
    /// </summary>
    public Uri? Location { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }
}
