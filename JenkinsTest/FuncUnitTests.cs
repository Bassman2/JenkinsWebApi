using System;
using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JenkinsTest
{
    [TestClass]
    public class FuncUnitTests
    {
        protected string host;
        protected string login;
        protected string password;

        public FuncUnitTests()
        {
            this.host = "http://b00007";
            this.login = "bs";
            this.password = "VisualEnte6.1Sp7";
        }

        [TestMethod]
        public void LogTest()
        {
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                string str = jenkins.GetComputerLogAsync("b00029").Result;
            }
        }

        [TestMethod]
        public void ScriptTest()
        {
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                string str = jenkins.RunComputerScriptAsync("b00029", "println InetAddress.localHost.hostName").Result;
            }
        }

        [TestMethod]
        public void LocalScriptTest()
        {
            using (Jenkins jenkins = new Jenkins("http://localhost:8080", "bs", "ente51"))
            {
                string str = jenkins.RunComputerScriptAsync("(master)", "println InetAddress.localHost.hostName").Result;
            }
        }

        [TestMethod]
        public void ComputerExtTest()
        {
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                JenkinsComputerExt info = jenkins.GetComputerExtAsync("deerscmvm068").Result;
            }
        }

        [TestMethod]
        public void LocalComputerExtTest()
        {
            using (Jenkins jenkins = new Jenkins("http://localhost:8080", "bs", "ente51"))
            {
                JenkinsComputerExt info = jenkins.GetComputerExtAsync("b00014").Result;
            }
        }
    }
}
