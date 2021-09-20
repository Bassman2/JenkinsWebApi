using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;

namespace JenkinsTest
{

    [TestClass]
    public class JenkinsWebApiDomUnitTest : JenkinsWebApiBaseUnitTest
    {
        

        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            JenkinsModelHudson server = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                server = jenkins.GetServerAsync().Result;
            }

            // Assert
            Assert.IsNotNull(server);
        }

    }
}
