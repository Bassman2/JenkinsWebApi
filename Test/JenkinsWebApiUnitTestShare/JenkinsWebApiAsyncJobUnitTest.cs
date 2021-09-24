using JenkinsWebApi;
using JenkinsWebApi.Model;
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
    public class JenkinsWebApiAsyncJobUnitTest : JenkinsWebApiBaseUnitTest
    {
        

        private void OnRunProgress(object sender, JenkinsRunProgress e)
        {
            Console.WriteLine($"Progess: {e.Status} {e.ProblemDescription}");
        }

        [TestMethod]
        public void JobRunSimpleImmediatelyTest()
        {
            // Arrange
            JenkinsRunProgress progress;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.RunConfig.RunMode = JenkinsRunMode.Immediately;
                progress = jenkins.RunJobAsync("FreeStyle").Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Queued, progress.Status, nameof(progress.Status));
        }

        [TestMethod]
        public void JobRunSimpleQueuedTest()
        {
            // Arrange
            JenkinsRunProgress progress;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.RunConfig.RunMode = JenkinsRunMode.Queued;
                progress = jenkins.RunJobAsync("FreeStyle").Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Queued, progress.Status, nameof(progress.Status));
        }

        [TestMethod]
        public void JobRunSimpleStartedTest()
        {
            // Arrange
            JenkinsRunProgress progress;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.RunConfig.RunMode = JenkinsRunMode.Started;
                progress = jenkins.RunJobAsync("FreeStyle").Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Building, progress.Status, nameof(progress.Status));
        }
        [TestMethod]
        public void JobRunSimpleFinishedTest()
        {
            // Arrange
            JenkinsRunProgress progress;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.RunConfig.RunMode = JenkinsRunMode.Finished;
                progress = jenkins.RunJobAsync("FreeStyle").Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Finished, progress.Status, nameof(progress.Status));
        }

        [TestMethod]
        public void JobRunOfflineTest()
        {
            // Arrange
            JenkinsRunProgress progress;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.RunProgress += OnRunProgress;
                progress = jenkins.RunJobAsync("FreestyleOffline").Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Stuck, progress.Status, nameof(progress.Status));
        }

        //[TestMethod]
        public void JobRunPeningTest()
        {
            // Arrange
            JenkinsRunProgress longRun;
            JenkinsRunProgress progress;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                longRun = jenkins.RunJobAsync("FreestyleRun1h", null, new JenkinsRunConfig() { RunMode = JenkinsRunMode.Started }, null, CancellationToken.None).Result;
                
                progress = jenkins.RunJobAsync("Freestyle").Result;

                jenkins.StopJobAsync(longRun.JobName, longRun.BuildNum).Wait();
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Stuck, progress.Status, nameof(progress.Status));
        }


        [TestMethod]
        public void JobRunDisabledTest()
        {
            // Arrange
            JenkinsRunProgress progress;
            
            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                progress = jenkins.RunJobAsync("FreestyleDisabled").Result;
            }
            
            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            Assert.AreEqual(JenkinsRunStatus.Disabled, progress.Status, nameof(progress.Status));
        }

        [TestMethod]
        public void JobRunParamTest()
        {
            // Arrange
            JenkinsRunProgress progress;
            JenkinsBuildParameters par = new JenkinsBuildParameters();
            par.Add("ParamA", "TestA");
            par.Add("ParamB", "TestB");
            par.Add("ParamC", "TestC");
            par.Add("CheckD", true);
            par.Add("CheckE", false);
            par.Add("TextBoxF", "TextF1\r\nTextF2\r\nTextF3");

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                progress = jenkins.RunJobAsync("FreestyleParam", par).Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            //Assert.IsNotNull(item.Url, "build.Result");
        }

        [TestMethod]
        public void JobRunFileTest()
        {
            // Arrange
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("This is a test file!");
            stream.Position = 0;

            JenkinsRunProgress progress;
            JenkinsBuildParameters par = new JenkinsBuildParameters();
            par.Add("ParamA", "");
            par.Add("ParamB", "");
            par.Add("ParamC", "");
            par.Add("CheckD", true);
            par.Add("CheckE", false);
            par.Add("TextBoxF", "Dies ist ein\r\nkleines Beispiel");
            par.Add("TestFile", stream, "FileName.bin");

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                progress = jenkins.RunJobAsync("FreestyleFile", par).Result;
            }

            // Assert
            Assert.IsNotNull(progress, nameof(progress));
            //Assert.IsNotNull(item.Url, "build.Result");
        }

        [TestMethod]
        public void JobDisableEnableTest()
        {
            // Arrange
            JenkinsModelFreeStyleProject freeStyleJobDisabled = null;
            JenkinsModelFreeStyleProject freeStyleJobEnabled = null;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.DisableJobAsync("FreestyleDisableEnable", CancellationToken.None).Wait();
                freeStyleJobDisabled = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("FreestyleDisableEnable").Result;
                jenkins.EnableJobAsync("FreestyleDisableEnable", CancellationToken.None).Wait();
                freeStyleJobEnabled = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("FreestyleDisableEnable").Result;
            }

            // Assert
            Assert.IsNotNull(freeStyleJobDisabled, nameof(freeStyleJobDisabled));
            Assert.IsTrue(freeStyleJobDisabled.IsDisabled, nameof(freeStyleJobDisabled.IsDisabled));

            Assert.IsNotNull(freeStyleJobEnabled, nameof(freeStyleJobEnabled));
            Assert.IsFalse(freeStyleJobEnabled.IsDisabled, nameof(freeStyleJobDisabled.IsDisabled));
        }

        [TestMethod]
        public void JobDescriptionTest()
        {
            // Arrange
            string descriptionDef = null;
            string descriptionTst = null;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                jenkins.SetJobDescriptionAsync("FreestyleDescription", "Default Description", CancellationToken.None).Wait();
                descriptionDef = jenkins.GetJobDescriptionAsync("FreestyleDescription", CancellationToken.None).Result;
                jenkins.SetJobDescriptionAsync("FreestyleDescription", "Test Description", CancellationToken.None).Wait();
                descriptionTst = jenkins.GetJobDescriptionAsync("FreestyleDescription", CancellationToken.None).Result;
                jenkins.SetJobDescriptionAsync("FreestyleDescription", "Default Description", CancellationToken.None).Wait();
            }

            // Assert
            Assert.AreEqual("Default Description", descriptionDef, nameof(descriptionDef));
            Assert.AreEqual("Test Description", descriptionTst, nameof(descriptionTst));
        }

        [TestMethod]
        public void JobConfigTest()
        {
            // Arrange
            string orgConfig = null;
            //string updConfig = null;
            //string cngConfig = null;
            //string descOrg = null;
            //string descCng = null;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                orgConfig = jenkins.GetJobConfigAsync("FreestyleConfig", CancellationToken.None).Result;
                //descOrg = GetConfigDescription(orgConfig);
                //cngConfig = SetConfigDescription(orgConfig, "Test Description");
                //jenkins.SetJobConfigAsync("FreeStyleConfig", cngConfig, CancellationToken.None).Wait();
                //updConfig = jenkins.GetJobConfigAsync("FreeStyleConfig", CancellationToken.None).Result;
                //descCng = GetConfigDescription(updConfig);
                //jenkins.SetJobConfigAsync("FreeStyleConfig", orgConfig, CancellationToken.None).Wait();

            }

            // Assert
            Assert.IsNotNull(orgConfig);
            //Assert.IsNotNull(updConfig);
            //Assert.IsNotNull(cngConfig);
            //Assert.AreEqual("Default Description", descOrg, nameof(descOrg));
            //Assert.AreEqual("Test Description", descCng, nameof(descCng));

        }

        private string GetConfigDescription(string config)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(config);
            XmlElement elm = doc.SelectSingleNode("description") as XmlElement;
            return elm.InnerText; 
        }

        private string SetConfigDescription(string config, string description)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(config);
            XmlElement elm = doc.SelectSingleNode("description") as XmlElement;
            elm.InnerText = description;
            return doc.ToString();
        }

        [TestMethod]
        public void JobCreateDeleteTextTest()
        {
            // Arrange
            string config;
            JenkinsModelFreeStyleProject freeStyleJob = null;
            bool exists;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                config = jenkins.GetJobConfigAsync("Freestyle").Result;
                jenkins.CreateJobAsync("Dummy", config).Wait();
                freeStyleJob = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("Dummy").Result;
                jenkins.DeleteJobAsync("Dummy").Wait();
                exists = jenkins.JobExists("Dummy").Result;
            }

            // Assert
            Assert.IsNotNull(freeStyleJob, nameof(freeStyleJob));
            Assert.IsFalse(exists, nameof(exists));
        }

        [TestMethod]
        public void JobCreateDeleteXmlTest()
        {
            // Arrange
            XmlDocument config;
            JenkinsModelFreeStyleProject freeStyleJob = null;
            bool exists;

            // Act
            using (Jenkins jenkins = new Jenkins(host, this.login, this.password))
            {
                config = jenkins.GetJobConfigXmlAsync("Freestyle").Result;
                jenkins.CreateJobAsync("Dummy", config).Wait();
                freeStyleJob = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("Dummy").Result;
                jenkins.DeleteJobAsync("Dummy").Wait();
                exists = jenkins.JobExists("Dummy").Result;
            }

            // Assert
            Assert.IsNotNull(freeStyleJob, nameof(freeStyleJob));
            Assert.IsFalse(exists, nameof(exists));
        }

        

        

        [TestMethod]
        public void JobGetTest()
        {
            // Arrange
            JenkinsModelFreeStyleProject freeStyleJob = null;
            JenkinsModelExternalJob externalJob = null;
            JenkinsMatrixMatrixProject matrixJob = null;
            JenkinsJenkinsciWorkflowJob workflowJob = null;
            JenkinsJenkinsciWorkflowMultiBranchProject multiBranchJob = null;
            JenkinsCloudbeesFolder folderJob = null;
            JenkinsBranchOrganizationFolder organizationFolderJob = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleJob = jenkins.GetJobAsync("FreeStyle").Result as JenkinsModelFreeStyleProject;
                externalJob = jenkins.GetJobAsync("ExternalJob").Result as JenkinsModelExternalJob;
                matrixJob = jenkins.GetJobAsync("Multiconfiguration").Result as JenkinsMatrixMatrixProject;
                workflowJob = jenkins.GetJobAsync("Pipeline").Result as JenkinsJenkinsciWorkflowJob;
                multiBranchJob = jenkins.GetJobAsync("MultibranchPipeline").Result as JenkinsJenkinsciWorkflowMultiBranchProject;
                folderJob = jenkins.GetJobAsync("Folder").Result as JenkinsCloudbeesFolder;
                organizationFolderJob = jenkins.GetJobAsync("GitHubOrganization").Result as JenkinsBranchOrganizationFolder;
            }

            // Assert
            Assert.IsNotNull(freeStyleJob);
            Assert.IsNotNull(externalJob);
            Assert.IsNotNull(matrixJob);
            Assert.IsNotNull(workflowJob);
            Assert.IsNotNull(multiBranchJob);
            Assert.IsNotNull(folderJob);
            Assert.IsNotNull(organizationFolderJob);



            Assert.IsNotNull(freeStyleJob.Actions, nameof(freeStyleJob.Actions));

            Assert.AreEqual("Project description", freeStyleJob.Description, nameof(freeStyleJob.Description));
            Assert.AreEqual("FreeStyle", freeStyleJob.DisplayName, nameof(freeStyleJob.DisplayName));
            Assert.AreEqual("FreeStyle", freeStyleJob.Name, nameof(freeStyleJob.Name));
            Assert.AreEqual($"{this.host}job/FreeStyle/", freeStyleJob.Url, nameof(freeStyleJob.Url));
            Assert.AreEqual(true, freeStyleJob.IsBuildable, nameof(freeStyleJob.IsBuildable));

            Assert.IsNotNull(freeStyleJob.Builds, nameof(freeStyleJob.Builds));

            //Assert.IsTrue(freeStyleJob.State.HasFlag(JenkinsJobState.Success), "color");

            Assert.IsNotNull(freeStyleJob.FirstBuild, nameof(freeStyleJob.FirstBuild));

            Assert.IsNotNull(freeStyleJob.HealthReports, nameof(freeStyleJob.HealthReports));
        }

        [TestMethod]
        [ExpectedJenkinsException(HttpStatusCode.NotFound)]
        public void JobGetFailedTest()
        {
            // Arrange
            JenkinsModelFreeStyleProject freeStyleJob = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleJob = jenkins.GetJobAsync("FreeStyleNotExists").Result as JenkinsModelFreeStyleProject;
            }

            // Assert
        }

        [TestMethod]
        public void JobGetGenericTest()
        {
            // Arrange
            JenkinsModelFreeStyleProject freeStyleJob = null;
            JenkinsModelExternalJob externalJob = null;
            JenkinsMatrixMatrixProject matrixJob = null;
            JenkinsJenkinsciWorkflowJob workflowJob = null;
            JenkinsJenkinsciWorkflowMultiBranchProject multiBranchJob = null;
            JenkinsCloudbeesFolder folderJob = null;
            JenkinsBranchOrganizationFolder organizationFolderJob = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleJob = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("FreeStyle").Result;
                externalJob = jenkins.GetJobAsync<JenkinsModelExternalJob>("ExternalJob").Result;
                matrixJob = jenkins.GetJobAsync<JenkinsMatrixMatrixProject>("Multiconfiguration").Result;
                workflowJob = jenkins.GetJobAsync<JenkinsJenkinsciWorkflowJob>("Pipeline").Result;
                multiBranchJob = jenkins.GetJobAsync<JenkinsJenkinsciWorkflowMultiBranchProject>("MultibranchPipeline").Result;
                folderJob = jenkins.GetJobAsync<JenkinsCloudbeesFolder>("Folder").Result;
                organizationFolderJob = jenkins.GetJobAsync<JenkinsBranchOrganizationFolder>("GitHubOrganization").Result;
            }

            // Assert
            Assert.IsNotNull(freeStyleJob);
            Assert.IsNotNull(externalJob);
            Assert.IsNotNull(matrixJob);
            Assert.IsNotNull(workflowJob);
            Assert.IsNotNull(multiBranchJob);
            Assert.IsNotNull(folderJob);
            Assert.IsNotNull(organizationFolderJob);
        }

        


        

       

        
        
    }
}
