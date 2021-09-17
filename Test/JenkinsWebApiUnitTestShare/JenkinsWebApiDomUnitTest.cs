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
    public class JenkinsWebApiDomUnitTest
    {
        protected readonly Uri host = new Uri("http://tiny:8080");
        //protected readonly string login = "Admin";
        //protected readonly string password = "admin";
        protected readonly string login = "Tester";
        protected readonly string password = "tester";
        //token name = TesterAPIToken
        protected readonly string token = "11096e7fa3b687e849ee95908b869058bc";
        // legacy token d65e88e40bb2bf5f72029713dce48243

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
