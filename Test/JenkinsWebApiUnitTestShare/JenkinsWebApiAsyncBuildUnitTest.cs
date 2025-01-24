

namespace JenkinsTest;

[TestClass]
public class JenkinsWebApiAsyncBuildUnitTest : JenkinsWebApiBaseUnitTest
{
    //[TestMethod]
    //public void BuildGetTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleBuild freeStyleBuild = null;
    //    //JenkinsBuildExternal externalBuild = null;
    //    JenkinsMatrixMatrixBuild matrixBuild = null;
    //    JenkinsJenkinsciWorkflowRun workflowBuild = null;
    //    //JenkinsBuildWorkflowMultiBranch multiBranchBuild = null;
    //    //JenkinsBuildFolder folderBuild = null;
    //    //JenkinsBuildOrganizationFolder organizationFolderBuild = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleBuild = jenkins.GetLastBuildAsync("Freestyle").Result as JenkinsModelFreeStyleBuild;
    //        //externalBuild = jenkins.GetBuildAsync("ExternalJob", 1).Result as JenkinsBuildExternal;
    //        matrixBuild = jenkins.GetBuildAsync("Multiconfiguration", 1).Result as JenkinsMatrixMatrixBuild;
    //        workflowBuild = jenkins.GetBuildAsync("Pipeline", 1).Result as JenkinsJenkinsciWorkflowRun;
    //        //multiBranchBuild = jenkins.GetBuildAsync("MultibranchPipeline", 1).Result as JenkinsBuildWorkflowMultiBranch;
    //        //folderBuild = jenkins.GetBuildAsync("Folder", 1).Result as JenkinsBuildFolder;
    //        //organizationFolderBuild = jenkins.GetBuildAsync("GitHubOrganization", 1).Result as JenkinsBuildOrganizationFolder;
    //    }

    //    // Assert
    //    Assert.IsNotNull(freeStyleBuild);
    //    //Assert.IsNotNull(externalBuild);
    //    Assert.IsNotNull(matrixBuild);
    //    Assert.IsNotNull(workflowBuild);
    //    //Assert.IsNotNull(multiBranchBuild);
    //    //Assert.IsNotNull(folderBuild);
    //    //Assert.IsNotNull(organizationFolderBuild);

    //}

    //[TestMethod]
    //public void BuildGetGenericTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleBuild freeStyleBuild = null;
    //    //JenkinsBuildExternal externalBuild = null;
    //    JenkinsMatrixMatrixBuild matrixBuild = null;
    //    JenkinsJenkinsciWorkflowRun workflowBuild = null;
    //    //JenkinsBuildWorkflowMultiBranch multiBranchBuild = null;
    //    //JenkinsBuildFolder folderBuild = null;
    //    //JenkinsBuildOrganizationFolder organizationFolderBuild = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleBuild = jenkins.GetLastBuildAsync<JenkinsModelFreeStyleBuild>("Freestyle").Result;
    //        //externalBuild = jenkins.GetBuildAsync("ExternalJob", 1).Result as JenkinsBuildExternal;
    //        matrixBuild = jenkins.GetBuildAsync<JenkinsMatrixMatrixBuild>("Multiconfiguration", 1).Result;
    //        workflowBuild = jenkins.GetBuildAsync<JenkinsJenkinsciWorkflowRun>("Pipeline", 1).Result;
    //        //multiBranchBuild = jenkins.GetBuildAsync("MultibranchPipeline", 1).Result as JenkinsBuildWorkflowMultiBranch;
    //        //folderBuild = jenkins.GetBuildAsync("Folder", 1).Result as JenkinsBuildFolder;
    //        //organizationFolderBuild = jenkins.GetBuildAsync("GitHubOrganization", 1).Result as JenkinsBuildOrganizationFolder;
    //    }

    //    // Assert
    //    Assert.IsNotNull(freeStyleBuild);
    //    //Assert.IsNotNull(externalBuild);
    //    Assert.IsNotNull(matrixBuild);
    //    Assert.IsNotNull(workflowBuild);
    //    //Assert.IsNotNull(multiBranchBuild);
    //    //Assert.IsNotNull(folderBuild);
    //    //Assert.IsNotNull(organizationFolderBuild);

    //}

    //[TestMethod]
    //public void BuildDeleteTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleProject freeStyleJob = null;
    //    int delBuildNum = 0;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleJob = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("FreeStyle").Result;
    //        delBuildNum = freeStyleJob.FirstBuild.Number;
    //        jenkins.DeleteBuildAsync("FreeStyle", delBuildNum).Wait();
    //        freeStyleJob = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("FreeStyle").Result;
    //    }

    //    // Assert
    //    Assert.IsNotNull(freeStyleJob, nameof(freeStyleJob));
    //    Assert.IsTrue(delBuildNum < freeStyleJob.FirstBuild.Number);
    //}

    //[TestMethod]
    //public void BuildConsoleOutputTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleBuild freeStyleBuild = null;
    //    string consoleOutput = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleBuild = jenkins.GetLastBuildAsync<JenkinsModelFreeStyleBuild>("FreeStyle").Result;

    //        consoleOutput = jenkins.GetBuildConsoleOutputAsync("FreeStyle", freeStyleBuild.Number, 0).Result;
    //    }

    //    // Assert
    //    Assert.IsNotNull(consoleOutput, nameof(consoleOutput));
    //    StringAssert.StartsWith(consoleOutput, "Started by user Tester");
    //}

    //[TestMethod]
    //public void BuildSetInformationTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleBuild freeStyleBuild = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleBuild = jenkins.GetLastBuildAsync<JenkinsModelFreeStyleBuild>("FreeStyle").Result;
    //        jenkins.SetBuildInformation("FreeStyle", freeStyleBuild.Number, "Build name", "Build description").Wait();
    //        freeStyleBuild = jenkins.GetBuildAsync<JenkinsModelFreeStyleBuild>("FreeStyle", freeStyleBuild.Number).Result;
    //        jenkins.SetBuildInformation("FreeStyle", freeStyleBuild.Number, "", "").Wait();
    //    }

    //    // Assert
    //    Assert.IsNotNull(freeStyleBuild, nameof(freeStyleBuild));

    //    Assert.AreEqual("Build name", freeStyleBuild.DisplayName, nameof(freeStyleBuild.DisplayName));
    //    Assert.AreEqual("Build description", freeStyleBuild.Description, nameof(freeStyleBuild.Description));
    //}

    //[TestMethod]
    //public void BuildEnvInjectVarListTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleBuild freeStyleBuild = null;
    //    JenkinsJenkinsciEnvInjectVarList varList = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleBuild = jenkins.GetLastBuildAsync<JenkinsModelFreeStyleBuild>("FreeStyle").Result;

    //        varList = jenkins.GetEnvInjectVarListAsync("FreeStyle", freeStyleBuild.Number).Result;
    //    }

    //    // Assert
    //    Assert.IsNotNull(varList, nameof(varList));
    //    var dict = varList.EnvMapDict;
    //    Assert.AreEqual("FreeStyle", dict["JOB_NAME"]);
    //    Assert.AreEqual(@"C:\Users\Public", dict["PUBLIC"]);
    //}


    //[TestMethod]
    //public void BuildGraphTest()
    //{
    //    // Arrange
    //    JenkinsModelFreeStyleBuild freeStyleBuild = null;
    //    JenkinsJenkinsciBuildGraph buildGraph = null;

    //    // Act
    //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
    //    {
    //        freeStyleBuild = jenkins.GetLastBuildAsync<JenkinsModelFreeStyleBuild>("FreestyleTree").Result;

    //        buildGraph = jenkins.GetBuildGraph("FreestyleTree", freeStyleBuild.Number).Result;
    //    }

    //    // Assert
    //    Assert.IsNotNull(buildGraph, nameof(buildGraph));
    //    Assert.IsNotNull(buildGraph.BuildGraph, nameof(buildGraph.BuildGraph));
    //}
}
