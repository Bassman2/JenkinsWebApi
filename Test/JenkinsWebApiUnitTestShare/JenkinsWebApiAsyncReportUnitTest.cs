using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsTest
{
    [TestClass]
    public class JenkinsWebApiAsyncReportUnitTest : JenkinsWebApiBaseUnitTest
    {
        [TestMethod]
        public void ReportTest()
        {
            //JenkinsTestResult report = null;
            //int number = -1;

            //using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            //{
            //    JenkinsJobFreeStyleProject job = jenkins.GetJobAsync(jobName).Result;
            //    number = job.Builds.Select(b => b.Number).LastOrDefault();
            //    report = jenkins.GetTestReportAsync(jobName, number).Result;
            //}

            //Assert.IsNotNull(report);
            //Assert.AreEqual(number, report..Number, "Number");
            //Assert.AreEqual($"{jobName} #{number}", report.DisplayName, "DisplayName");
        }
    }
}
