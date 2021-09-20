using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsTest
{
    [TestClass]
    public class JenkinsWebApiAsyncUserUnitTest : JenkinsWebApiBaseUnitTest
    {
        [TestMethod]
        public void PeopleTest()
        {
            // Arrange
            JenkinsModelViewPeople people = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                people = jenkins.GetPeopleAsync().Result;
            }

            // Assert
            Assert.IsNotNull(people, nameof(people));
            Assert.AreEqual("hudson.model.View$People", people.Class, nameof(people.Class));

            Assert.IsNotNull(people.Users, nameof(people.Users));

            var user = people.Users.Single(u => u.User.FullName == "Tester");
            Assert.IsNotNull(user, nameof(user));
            Assert.AreEqual($"{this.host}user/tester", user.User.AbsoluteUrl, nameof(user.User.AbsoluteUrl));
            Assert.AreEqual("Tester", user.User.FullName, nameof(user.User.FullName));
        }

        [TestMethod]
        public void UserTest()
        {
            // Arrange
            JenkinsModelUser user = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                user = jenkins.GetUserAsync("Tester").Result;
            }

            // Assert
            Assert.IsNotNull(user, nameof(user));

            Assert.AreEqual("hudson.model.User", user.Class, nameof(user.Class));
            Assert.AreEqual($"{this.host}user/tester", user.AbsoluteUrl, nameof(user.AbsoluteUrl));

            Assert.AreEqual("tester", user.Id, nameof(user.Id));
            Assert.AreEqual("Tester", user.FullName, nameof(user.FullName));
            Assert.AreEqual("Test User", user.Description, nameof(user.Description));
        }

        [TestMethod]
        public void CurrentUserTest()
        {
            // Arrange
            JenkinsModelUser user = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                user = jenkins.GetCurrentUserAsync().Result;
            }

            // Assert
            Assert.IsNotNull(user, nameof(user));

            Assert.AreEqual("hudson.model.User", user.Class, nameof(user.Class));
            Assert.AreEqual($"{this.host}user/tester", user.AbsoluteUrl, nameof(user.AbsoluteUrl));

            Assert.AreEqual("tester", user.Id, nameof(user.Id));
            Assert.AreEqual("Tester", user.FullName, nameof(user.FullName));
            Assert.AreEqual("Test User", user.Description, nameof(user.Description));
        }
    }
}
