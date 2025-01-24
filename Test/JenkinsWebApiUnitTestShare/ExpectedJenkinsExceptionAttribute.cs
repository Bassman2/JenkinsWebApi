namespace JenkinsTest;

//public class ExpectedJenkinsExceptionAttribute : ExpectedExceptionBaseAttribute
//{
//    public HttpStatusCode StatusCode { get; }

//    public ExpectedJenkinsExceptionAttribute(HttpStatusCode statusCode)
//    {
//        this.StatusCode = statusCode;
//    }
    
//    protected override void Verify(Exception e)
//    {
//        if (e.GetType() == typeof(AggregateException))
//        {
//            e = e.InnerException;
//        }

//        if (e is JenkinsException ex)
//        {
//            if (ex.StatusCode != this.StatusCode)
//            {
//                Assert.Fail($"ExpectedJenkinsExceptionAttribute failed. Expected status code: <{this.StatusCode}>. Actual status code: <{ex.StatusCode}>. Exception reason: <{ex.Reason}>. Exception message: <{ex.Message}>");
//            }
//        }
//        else
//        {
//            Assert.Fail($"ExpectedJenkinsExceptionAttribute failed. Expected exception type: <{typeof(JenkinsException).FullName}>. Actual exception type: <{e.GetType().FullName}>. Exception message: <{e.Message}>");
//        }
//    }
//}
