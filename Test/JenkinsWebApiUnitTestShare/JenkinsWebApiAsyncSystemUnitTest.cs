namespace JenkinsTest;

[TestClass]
public class JenkinsWebApiAsyncSystemUnitTest : JenkinsWebApiBaseUnitTest
{
    //[TestMethod]
    //public void ServerTest()
    //{
    //    // Arrange
    //    JenkinsModelHudson server = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        server = jenkins.GetServerAsync().Result;
    //    }

    //    // Assert
    //    Assert.IsNotNull(server, nameof(server));
    //    Assert.AreEqual("hudson.model.Hudson", server.Class, nameof(server.Class));

    //    Assert.AreEqual("Hello World", server.Description, nameof(server.Description));
    //    Assert.AreEqual(0, server.SlaveAgentPort, nameof(server.SlaveAgentPort));
    //    Assert.AreEqual($"{this.host}", server.Url, nameof(server.Url));
    //    Assert.AreEqual(true, server.UseCrumbs, nameof(server.UseCrumbs));
    //    Assert.AreEqual(true, server.UseSecurity, nameof(server.UseSecurity));

    //    Assert.AreEqual(JenkinsModelNodeMode.Normal, server.Mode, nameof(server.Mode));
    //    Assert.AreEqual("the master Jenkins node", server.NodeDescription, nameof(server.NodeDescription));
    //    Assert.AreEqual("", server.NodeName, nameof(server.NodeName));
    //    Assert.AreEqual(1, server.NumExecutors, nameof(server.NumExecutors));

    //    Assert.IsNotNull(server.AssignedLabels, nameof(server.AssignedLabels));
    //    Assert.IsNotNull(server.Jobs, nameof(server.Jobs));
    //    Assert.IsNotNull(server.OverallLoad, nameof(server.OverallLoad));
    //    Assert.IsNotNull(server.PrimaryView, nameof(server.PrimaryView));
    //    Assert.IsNotNull(server.Views, nameof(server.Views));
    //}

    //[TestMethod]
    //public void CredentialsTest()
    //{
    //    JenkinsCloudbeesViewCredentialsActionRootActionImpl credentials;

    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        credentials = jenkins.GetCredentialsAsync().Result;
    //    }

    //    Assert.IsNotNull(credentials, "credentials");

    //}
}
