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
    public class JenkinsWebApiAsyncUnitTest : JenkinsWebApiBaseUnitTest
    {
        //protected readonly Uri host = new Uri("http://tiny:8080");
        //protected readonly string login = "Tester";
        //protected readonly string password = "tester";
        //protected readonly string token = "11096e7fa3b687e849ee95908b869058bc";

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
        [ExpectedJenkinsException(HttpStatusCode.Unauthorized)]
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

        // Feature removed in newer Jenkins versions
        //[TestMethod]
        //public void InstancesTest()
        //{
        //    List<JenkinsInstance> list = Jenkins.GetJenkinsInstancesAsync().Result?.ToList();

        //    Assert.IsNotNull(list, "list");
        //    Assert.IsTrue(list.Count > 0, "list.Count");
        //}

        [TestMethod]
        public void ServerTest()
        {
            // Arrange
            JenkinsModelHudson server = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                server = jenkins.GetServerAsync().Result;
            }

            // Assert
            Assert.IsNotNull(server, nameof(server));
            Assert.AreEqual("hudson.model.Hudson", server.Class, nameof(server.Class));
            
            Assert.AreEqual("Hello World", server.Description, nameof(server.Description));
            Assert.AreEqual(0, server.SlaveAgentPort, nameof(server.SlaveAgentPort));
            Assert.AreEqual($"{this.host}", server.Url, nameof(server.Url));
            Assert.AreEqual(true, server.UseCrumbs, nameof(server.UseCrumbs));
            Assert.AreEqual(true, server.UseSecurity, nameof(server.UseSecurity));

            Assert.AreEqual(JenkinsModelNodeMode.Normal, server.Mode, nameof(server.Mode));
            Assert.AreEqual("the master Jenkins node", server.NodeDescription, nameof(server.NodeDescription));
            Assert.AreEqual("", server.NodeName, nameof(server.NodeName));
            Assert.AreEqual(1, server.NumExecutors, nameof(server.NumExecutors));

            Assert.IsNotNull(server.AssignedLabels, nameof(server.AssignedLabels));
            Assert.IsNotNull(server.Jobs, nameof(server.Jobs));
            Assert.IsNotNull(server.OverallLoad, nameof(server.OverallLoad));
            Assert.IsNotNull(server.PrimaryView, nameof(server.PrimaryView));
            Assert.IsNotNull(server.Views, nameof(server.Views));
        }


        //[TestMethod]
        //public void ServerTest()
        //{
        //    JenkinsHudson server = null;

        //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
        //    {
        //        server = jenkins.GetServerAsync().Result;
        //    }

        //    Assert.IsNotNull(server, "server");
        //    Assert.IsNotNull(server.AssignedLabels, "assignedLabels");

        //    Assert.AreEqual(this.serverMode, server.Mode, "mode");
        //    Assert.AreEqual("Jenkins Master-Knoten", server.NodeDescription, "nodeDescription");
        //    Assert.AreEqual("", server.NodeName, "nodeName");
        //    Assert.AreEqual(2, server.NumExecutors, "numExecutors");
        //    Assert.AreEqual("System Message", server.Description, "description");

        //    //Assert.IsNotNull(server.Jobs, "jobs");

        //    Assert.IsNotNull(server.OverallLoad, "overallLoad");

        //    Assert.IsNotNull(server.PrimaryView, "primaryView");

        //    Assert.AreEqual(-1, server.SlaveAgentPort, "slaveAgentPort");
        //    Assert.AreEqual(true, server.UseCrumbs, "useCrumbs");
        //    Assert.AreEqual(true, server.UseSecurity, "useSecurity");

        //    Assert.IsNotNull(server.Views, "views");

        //    Assert.AreEqual("", server.NodeName, "nodeName");
        //}

        [TestMethod]
        public void CredentialsTest()
        {
            JenkinsCloudbeesViewCredentialsActionRootActionImpl credentials;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                credentials = jenkins.GetCredentialsAsync().Result;
            }

            Assert.IsNotNull(credentials, "credentials");
            
        }

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

        [TestMethod]
        public void ViewGetTest()
        {
            // Arrange            
            JenkinsModelListView listView = null;
            JenkinsModelMyView myView = null;
            JenkinsModelAllView allView = null;
            JenkinsJenkinsciCategorizedJobsView categorizedJobsView = null;
            JenkinsPluginsViewDashboardDashboard dashboard = null;
            JenkinsTikalMultiJobView multiJobView = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                listView = jenkins.GetViewAsync("ListView").Result as JenkinsModelListView;
                myView = jenkins.GetViewAsync("MyView").Result as JenkinsModelMyView;
                allView = jenkins.GetViewAsync("all").Result as JenkinsModelAllView;
                categorizedJobsView = jenkins.GetViewAsync("Categorization").Result as JenkinsJenkinsciCategorizedJobsView;
                dashboard = jenkins.GetViewAsync("Dashboard").Result as JenkinsPluginsViewDashboardDashboard;
                multiJobView = jenkins.GetViewAsync("MultiJob").Result as JenkinsTikalMultiJobView;
            }

            // Assert
            Assert.IsNotNull(listView, nameof(listView));
            Assert.AreEqual("hudson.model.ListView", listView.Class, nameof(listView.Class));
            Assert.AreEqual("ListView", listView.Name, nameof(listView.Name));
            Assert.AreEqual("ListView Description", listView.Description, nameof(listView.Description));
            Assert.AreEqual($"{this.host}view/ListView/", listView.Url, nameof(listView.Url));

            Assert.IsNotNull(myView, nameof(myView));
            Assert.IsNotNull(allView, nameof(allView));
            Assert.IsNotNull(categorizedJobsView, nameof(categorizedJobsView));
            Assert.IsNotNull(dashboard, nameof(dashboard));
            Assert.IsNotNull(multiJobView, nameof(multiJobView));
        }

        [TestMethod]
        public void ViewGetGenericTest()
        {
            // Arrange            
            JenkinsModelListView listView = null;
            JenkinsModelMyView myView = null;
            JenkinsModelAllView allView = null;
            JenkinsJenkinsciCategorizedJobsView categorizedJobsView = null;
            JenkinsPluginsViewDashboardDashboard dashboard = null;
            JenkinsTikalMultiJobView multiJobView = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                listView = jenkins.GetViewAsync<JenkinsModelListView>("ListView").Result;
                myView = jenkins.GetViewAsync<JenkinsModelMyView>("MyView").Result;
                allView = jenkins.GetViewAsync<JenkinsModelAllView>("all").Result;
                categorizedJobsView = jenkins.GetViewAsync<JenkinsJenkinsciCategorizedJobsView>("Categorization").Result;
                dashboard = jenkins.GetViewAsync<JenkinsPluginsViewDashboardDashboard>("Dashboard").Result;
                multiJobView = jenkins.GetViewAsync<JenkinsTikalMultiJobView>("MultiJob").Result;
            }

            // Assert
            Assert.IsNotNull(listView, nameof(listView));
            Assert.AreEqual("hudson.model.ListView", listView.Class, nameof(listView.Class));
            Assert.AreEqual("ListView", listView.Name, nameof(listView.Name));
            Assert.AreEqual("ListView Description", listView.Description, nameof(listView.Description));
            Assert.AreEqual($"{this.host}view/ListView/", listView.Url, nameof(listView.Url));

            Assert.IsNotNull(myView, nameof(myView));
            Assert.IsNotNull(allView, nameof(allView));
            Assert.IsNotNull(categorizedJobsView, nameof(categorizedJobsView));
            Assert.IsNotNull(dashboard, nameof(dashboard));
            Assert.IsNotNull(multiJobView, nameof(multiJobView));
        }

        [TestMethod]
        public void JobGetTest()
        {
            // Arrange
            JenkinsModelFreeStyleProject freeStyleJob = null;
            JenkinsModelExternalJob externalJob = null;
            JenkinsMatrixMatrixProject matrixJob = null;
            JenkinsJenkinsciWorkflowJob workflowJob = null;
            //JenkinsJenkinsciWorkflowMultiBranchProject multiBranchJob = null;
            //JenkinsCloudbeesFolder folderJob = null;
            //JenkinsBranchOrganizationFolder organizationFolderJob = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleJob = jenkins.GetJobAsync("FreeStyle").Result as JenkinsModelFreeStyleProject;
                externalJob = jenkins.GetJobAsync("ExternalJob").Result as JenkinsModelExternalJob;
                matrixJob = jenkins.GetJobAsync("Multiconfiguration").Result as JenkinsMatrixMatrixProject;
                workflowJob = jenkins.GetJobAsync("Pipeline").Result as JenkinsJenkinsciWorkflowJob;
                //multiBranchJob = jenkins.GetJobAsync("MultibranchPipeline").Result as JenkinsJenkinsciWorkflowMultiBranchProject;
                //folderJob = jenkins.GetJobAsync("Folder").Result as JenkinsCloudbeesFolder;
                //organizationFolderJob = jenkins.GetJobAsync("GitHubOrganization").Result as JenkinsBranchOrganizationFolder;
            }

            // Assert
            Assert.IsNotNull(freeStyleJob);
            Assert.IsNotNull(externalJob);
            Assert.IsNotNull(matrixJob);
            Assert.IsNotNull(workflowJob);
            //Assert.IsNotNull(multiBranchJob);
            //Assert.IsNotNull(folderJob);
            //Assert.IsNotNull(organizationFolderJob);



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
            //JenkinsJenkinsciWorkflowMultiBranchProject multiBranchJob = null;
            //JenkinsCloudbeesFolder folderJob = null;
            //JenkinsBranchOrganizationFolder organizationFolderJob = null;

            // Act
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleJob = jenkins.GetJobAsync<JenkinsModelFreeStyleProject>("FreeStyle").Result;
                externalJob = jenkins.GetJobAsync<JenkinsModelExternalJob>("ExternalJob").Result;
                matrixJob = jenkins.GetJobAsync<JenkinsMatrixMatrixProject>("Multiconfiguration").Result;
                workflowJob = jenkins.GetJobAsync<JenkinsJenkinsciWorkflowJob>("Pipeline").Result;
                //multiBranchJob = jenkins.GetJobAsync<JenkinsJenkinsciWorkflowMultiBranchProject>("MultibranchPipeline").Result;
                //folderJob = jenkins.GetJobAsync<JenkinsCloudbeesFolder>("Folder").Result;
                //organizationFolderJob = jenkins.GetJobAsync<JenkinsBranchOrganizationFolder>("GitHubOrganization").Result;
            }

            // Assert
            Assert.IsNotNull(freeStyleJob);
            Assert.IsNotNull(externalJob);
            Assert.IsNotNull(matrixJob);
            Assert.IsNotNull(workflowJob);
            //Assert.IsNotNull(multiBranchJob);
            //Assert.IsNotNull(folderJob);
            //Assert.IsNotNull(organizationFolderJob);
        }

        


        //[TestMethod]
        //public void RunTestXXXXX()
        //{
        //    JenkinsModelRun build;

        //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
        //    {
        //        build = jenkins.RunJobComplete("Freestyle Test Pure");
        //    }

        //    //Assert.AreEqual(JenkinsResult.Success, build.Result, "build.Result");
        //}

        //[TestMethod]
        //public void RunParamTestXXXXX()
        //{
        //    JenkinsModelRun build;
        //    using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
        //    {
        //        JenkinsBuildParameters par = new JenkinsBuildParameters();
        //        par.Add("ParamA", "TestA");
        //        par.Add("ParamB", "TestB");
        //        par.Add("ParamC", "TestC");
        //        par.Add("CheckD", false);
        //        par.Add("CheckE", true);
        //        par.Add("TextBoxF", "TextF1\\nTextF2\\nTextF3");

        //        build = jenkins.RunJobComplete("Freestyle Test Parameter", par);
        //    }

        //    //Assert.AreEqual(JenkinsResult.Success, build.Result, "build.Result");
        //}

        //[TestMethod]
        //public void RunFileTest()
        //{
        //    //JenkinsBuild build;
        //    //using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
        //    //{
        //    //    using (FileStream file = File.OpenRead(filePath))
        //    //    {
        //    //        JenkinsBuildParameters par = new JenkinsBuildParameters();
        //    //        par.Add("AppStartATF.core.gz", file, filePath);
        //    //        par.Add("NavVersion", "");
        //    //        par.Add("CoreDescription", "");
        //    //        build = jenkins.RunJobComplete(jobName, par);
        //    //    }
        //    //}

        //    //Assert.AreEqual(JenkinsBuildResult.Success, build.Result, "build.Result");
        //    //Assert.AreNotEqual(0, build.Number, "Number");
        //}

        [TestMethod]
        public void ReportTest()
        {
            //JenkinsTestResult report = null;
            //int number = -1;

            //using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            //{
            //    JenkinsJobFreeStyleProject job = jenkins.GetJobAsync(jobName).Result;
            //    number = job.Builds.Select(b => b.Number).LastOrDefault();
            //    report = jenkins.GetTestReportAsync(jobName, number).Result;
            //}

            //Assert.IsNotNull(report);
            //Assert.AreEqual(number, report..Number, "Number");
            //Assert.AreEqual($"{jobName} #{number}", report.DisplayName, "DisplayName");
        }

        [TestMethod]
        public void RunUserTest()
        {
            
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                string user = jenkins.GetComputerUserAsync("(master)").Result;
            }

            //Assert.AreEqual(JenkinsResult.Success, build.Result, "build.Result");
        }

        [TestMethod]
        public void GetComputerSetTest()
        {
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                JenkinsModelComputerSet set = jenkins.GetComputerSetAsync().Result;
            }

            
        }
        
    }
}
