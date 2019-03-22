using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JenkinsWebApi;
using JenkinsWebApi.Model;

namespace JenkinsTest
{
    [TestClass]
    public class LocalUnitTest : BaseUnitTest
    {


        public LocalUnitTest()
        {
            this.host = "http://localhost:8080";
            this.login = "";
            this.password = "";

            //this.serverMode = JenkinsNodeMode.Normal;
        }

        //[TestMethod]
        //public void ServerTest()
        //{
        //    JenkinsHudson server = null;

        //    using (Jenkins jenkins = new Jenkins(new Uri(host), this.login, this.password))
        //    {
        //        server = jenkins.GetServerAsync().Result;
        //    }

        //    Assert.IsNotNull(server);
        //    Assert.IsNotNull(server.AssignedLabels, "assignedLabels");

        //    Assert.AreEqual("NORMAL", server.Mode, "mode");
        //    Assert.AreEqual("the master Jenkins node", server.NodeDescription, "nodeDescription");
        //    Assert.AreEqual("", server.NodeName, "nodeName");
        //    Assert.AreEqual(2, server.NumExecutors, "numExecutors");
        //    Assert.AreEqual(null, server.Description, "description");

        //    Assert.IsNotNull(server.Jobs, "jobs");

        //    Assert.IsNotNull(server.OverallLoad, "overallLoad");

        //    Assert.IsNotNull(server.PrimaryView, "primaryView");

        //    Assert.AreEqual(-1, server.SlaveAgentPort, "slaveAgentPort");
        //    Assert.AreEqual(true, server.UseCrumbs, "useCrumbs");
        //    Assert.AreEqual(true, server.UseSecurity, "useSecurity");

        //    Assert.IsNotNull(server.Views, "views");

        //    Assert.AreEqual("", server.NodeName, "nodeName");
        //}

        [TestMethod]
        public void RunTest()
        {
            //JenkinsRun build;

            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                /*build =*/var x= jenkins.RunJobAsync("Freestyle Test Pure").Result;
            }

            //Assert.AreEqual(JenkinsResult.Success, build.Result, "build.Result");
        }

        [TestMethod]
        public void RunParamTest()
        {
            JenkinsRun build;
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                JenkinsBuildParameters par = new JenkinsBuildParameters();
                par.Add("ParamA", "");
                par.Add("ParamB", "");
                par.Add("ParamC", "");
                par.Add("CheckD", true);
                par.Add("CheckE", false);
                par.Add("TextBoxF", "Dies ist ein\r\nkleines Beispiel");

                build = jenkins.RunJobComplete("Freestyle Test Parameter", par);
            }

            //Assert.AreEqual(JenkinsResult.Failure, build.Result, "build.Result");
        }
    }
}
