using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JenkinsTest
{
    [TestClass]
    public class JenkinsWebApiAsyncLoginUnitTest : JenkinsWebApiBaseUnitTest
    {
        [TestMethod]
        public void LoginPasswordTest()
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

        [TestMethod]
        public void LoginTokenTest()
        {
            // Arrange
            JenkinsModelHudson server = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.token))
            {
                server = jenkins.GetServerAsync().Result;
            }

            // Assert
            Assert.IsNotNull(server);
        }

        [TestMethod]
        public void LoginLateTest()
        {
            // Arrange
            JenkinsModelHudson server = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host))
            {
                jenkins.Login(this.login, this.password);
                server = jenkins.GetServerAsync().Result;
            }

            // Assert
            Assert.IsNotNull(server);
        }

        [TestMethod]
        [ExpectedAggregateException(typeof(JenkinsUnauthorizedException))]
        public void LoginFailedTest()
        {
            // Arrange
            JenkinsModelHudson server = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, "Mustermann", "Max"))
            {
                server = jenkins.GetServerAsync().Result;
            }

            // Assert
        }

        [TestMethod]
        [ExpectedAggregateException(typeof(JenkinsForbiddenException))]
        public void LoginEmptyTest()
        {
            // Arrange
            JenkinsModelHudson server = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host))
            {
                server = jenkins.GetServerAsync().Result;
            }

            // Assert
        }
    }
}
