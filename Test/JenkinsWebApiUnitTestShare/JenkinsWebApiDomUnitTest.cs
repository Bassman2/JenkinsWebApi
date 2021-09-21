using JenkinsWebApi;
using JenkinsWebApi.Model;
using JenkinsWebApi.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;

namespace JenkinsTest
{

    [TestClass]
    public class JenkinsWebApiDomUnitTest : JenkinsWebApiBaseUnitTest
    {
        

        [TestMethod]
        public void LoginSuccessTest()
        {
            // Arrange
            Uri serverUrl;
            string serverDescription;

            // Act
            using (JenkinsServer jenkins = new JenkinsServer(this.host, this.login, this.password))
            {
                serverUrl = jenkins.Url;
                serverDescription = jenkins.Description;
            }

            // Assert
            Assert.AreEqual(this.host, serverUrl, nameof(serverUrl));
            Assert.AreEqual("Hello World", serverDescription, nameof(serverDescription));
        }

        [TestMethod]
        //[ExpectedJenkinsException(HttpStatusCode.Unauthorized)]
        [ExpectedException(typeof(JenkinsUnauthorizedException))]
        public void LoginFailureTest()
        {
            // Arrange

            // Act
            using (JenkinsServer jenkins = new JenkinsServer(this.host, this.login, "aaaaaaaaaaaaaaaa"))
            {
            }

            // Assert
            Assert.IsNotNull(null);
        }
    }
}
