using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace JenkinsTest
{
    public abstract class BaseUnitTest
    {
        protected string host;
        protected string login;
        protected string password;

        ///protected JenkinsNodeMode serverMode = JenkinsNodeMode.Normal;

        [TestMethod]
        public void LoginTest()
        {
            bool failure;
            bool success;

            using (Jenkins jenkins = new Jenkins(this.host))
            {
                failure = jenkins.Login(this.login, "xxxxx");
                success = jenkins.Login(this.login, this.password);
            }

            Assert.IsFalse(failure, "failure");
            Assert.IsTrue(success, "success");
        }

        [TestMethod]
        public void InstancesTest()
        {
            List<JenkinsInstance> list = Jenkins.GetJenkinsInstances().Result.ToList();

            Assert.IsNotNull(list, "list");
            Assert.IsTrue(list.Count > 0, "list.Count");
        }

        [TestMethod]
        public void ServerTest()
        {
            JenkinsHudson server = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                server = jenkins.GetServerAsync().Result;
            }

            Assert.IsNotNull(server);
            Assert.IsNotNull(server.AssignedLabels, "assignedLabels");

            //Assert.AreEqual(this.serverMode, server.Mode, "mode");
            Assert.AreEqual("the master Jenkins node", server.NodeDescription, "nodeDescription");
            Assert.AreEqual("", server.NodeName, "nodeName");
            Assert.AreEqual(2, server.NumExecutors, "numExecutors");
            Assert.AreEqual(null, server.Description, "description");

            Assert.IsNotNull(server.Jobs, "jobs");

            Assert.IsNotNull(server.OverallLoad, "overallLoad");

            Assert.IsNotNull(server.PrimaryView, "primaryView");

            Assert.AreEqual(-1, server.SlaveAgentPort, "slaveAgentPort");
            Assert.AreEqual(true, server.UseCrumbs, "useCrumbs");
            Assert.AreEqual(true, server.UseSecurity, "useSecurity");

            Assert.IsNotNull(server.Views, "views");

            Assert.AreEqual("", server.NodeName, "nodeName");
        }

    }
}
