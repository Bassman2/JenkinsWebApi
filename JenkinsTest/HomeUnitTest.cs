using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JenkinsTest
{
    [TestClass]
    public class HomeUnitTest : BaseUnitTest
    {

        

        public HomeUnitTest()
        {
            this.host = "http://localhost:8080";
            this.login = "xx";
            this.password = "xx";

            //this.serverMode = JenkinsNodeMode.Normal;
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
            JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl credentials;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                credentials = jenkins.GetCredentialsAsync().Result;
            }

            Assert.IsNotNull(credentials, "credentials");
            
        }

        [TestMethod]
        public void PeopleTest()
        {
            JenkinsModelViewPeople people = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                people = jenkins.GetPeopleAsync().Result;
            }

            Assert.IsNotNull(people, "people");
            Assert.AreEqual("hudson.model.View$AsynchPeople$People", people.Class, "people.Class");

            Assert.IsNotNull(people.Users, "people.Users");

            var user = people.Users.Single(u => u.User.FullName == "User");
            Assert.IsNotNull(user, "user");
            Assert.AreEqual($"{this.host}/user/xx", user.User.AbsoluteUrl, "user.User.AbsoluteUrl");
            Assert.AreEqual("xx", user.User.FullName, "user.User.FullName");
        }

        [TestMethod]
        public void UserTest()
        {
            JenkinsModelUser user = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                user = jenkins.GetUserAsync("User").Result;
            }

            Assert.IsNotNull(user, "user");
            Assert.AreEqual($"{this.host}/user/xx", user.AbsoluteUrl, "user.AbsoluteUrl");
            Assert.AreEqual(null, user.Description, "user.Description");
            Assert.AreEqual("User", user.FullName, "user.FullName");
            Assert.AreEqual("User", user.Id, "user.Id");
            
            
        }

        [TestMethod]
        public void CurrentUserTest()
        {
            JenkinsModelUser user = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                user = jenkins.GetCurrentUserAsync().Result;
            }
            
            Assert.IsNotNull(user, "user");
            Assert.AreEqual($"{this.host}/user/xx", user.AbsoluteUrl, "user.AbsoluteUrl");
            Assert.AreEqual(null, user.Description, "user.Description");
            Assert.AreEqual("xxx", user.FullName, "user.FullName");
            Assert.AreEqual("xx", user.Id, "user.Id");

            
        }

        [TestMethod]
        public void ViewTest()
        {
            string viewName = "View 2";
            JenkinsModelView view = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                view = jenkins.GetViewAsync(viewName).Result;
            }

            Assert.IsNotNull(view);
            Assert.AreEqual(viewName, view.Name, "Name");
        }

        [TestMethod]
        public void JobTest()
        {
            string jobName = "Freestyle Test Parameter";
            
            JenkinsModelFreeStyleProject freeStyleJob = null;
            JenkinsModelExternalJob externalJob = null;
            JenkinsMatrixMatrixProject matrixJob = null;
            JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowJob workflowJob = null;
            JenkinsOrgJenkinsciPluginsWorkflowMultibranchWorkflowMultiBranchProject multiBranchJob = null;
            JenkinsComCloudbeesHudsonPluginsFolderFolder folderJob = null;
            JenkinsBranchOrganizationFolder organizationFolderJob = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleJob = jenkins.GetJobAsync("Freestyle Test Pure").Result as JenkinsModelFreeStyleProject;
                externalJob = jenkins.GetJobAsync("External Job").Result as JenkinsModelExternalJob;
                matrixJob = jenkins.GetJobAsync("Multiconfiguration").Result as JenkinsMatrixMatrixProject;
                workflowJob = jenkins.GetJobAsync("Pipeline").Result as JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowJob;
                multiBranchJob = jenkins.GetJobAsync("Multibranch").Result as JenkinsOrgJenkinsciPluginsWorkflowMultibranchWorkflowMultiBranchProject;
                folderJob = jenkins.GetJobAsync("Folder").Result as JenkinsComCloudbeesHudsonPluginsFolderFolder;
                organizationFolderJob = jenkins.GetJobAsync("GitHub").Result as JenkinsBranchOrganizationFolder;
            }

            Assert.IsNotNull(freeStyleJob);
            Assert.IsNotNull(externalJob);
            Assert.IsNotNull(matrixJob);
            Assert.IsNotNull(workflowJob);
            Assert.IsNotNull(multiBranchJob);
            Assert.IsNotNull(folderJob);
            Assert.IsNotNull(organizationFolderJob);



            Assert.IsNotNull(freeStyleJob.Actions, "actions");

            Assert.AreEqual("Test with Parameters", freeStyleJob.Description, "description");
            Assert.AreEqual(jobName, freeStyleJob.DisplayName, "job.DisplayName");
            Assert.AreEqual(jobName, freeStyleJob.Name, "job.Name");
            Assert.AreEqual($"{this.host}/job/Freestyle%20Test%20Parameter/", freeStyleJob.Url, "job.Url");
            Assert.AreEqual(true, freeStyleJob.IsBuildable, "job.IsBuildable");

            Assert.IsNotNull(freeStyleJob.Builds, "builds");

            //Assert.IsTrue(freeStyleJob.State.HasFlag(JenkinsJobState.Success), "color");

            Assert.IsNotNull(freeStyleJob.FirstBuild, "firstBuild");

            Assert.IsNotNull(freeStyleJob.HealthReports, "healthReport");
        }

        [TestMethod]
        public void BuildTest()
        {
            JenkinsModelFreeStyleBuild freeStyleBuild = null;
            //JenkinsBuildExternal externalBuild = null;
            JenkinsMatrixMatrixBuild matrixBuild = null;
            JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun workflowBuild = null;
            //JenkinsBuildWorkflowMultiBranch multiBranchBuild = null;
            //JenkinsBuildFolder folderBuild = null;
            //JenkinsBuildOrganizationFolder organizationFolderBuild = null;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                freeStyleBuild = jenkins.GetLastBuildAsync("Freestyle Test Pure").Result as JenkinsModelFreeStyleBuild;
                //externalBuild = jenkins.GetBuildAsync("External Job", 1).Result as JenkinsBuildExternal;
                matrixBuild = jenkins.GetBuildAsync("Multiconfiguration", 1).Result as JenkinsMatrixMatrixBuild;
                workflowBuild = jenkins.GetBuildAsync("Pipeline", 1).Result as JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun;
                //multiBranchBuild = jenkins.GetBuildAsync("Multibranch", 1).Result as JenkinsBuildWorkflowMultiBranch;
                //folderBuild = jenkins.GetBuildAsync("Folder", 1).Result as JenkinsBuildFolder;
                //organizationFolderBuild = jenkins.GetBuildAsync("GitHub", 1).Result as JenkinsBuildOrganizationFolder;
            }

            Assert.IsNotNull(freeStyleBuild);
            //Assert.IsNotNull(externalBuild);
            Assert.IsNotNull(matrixBuild);
            //Assert.IsNotNull(workflowBuild);
            //Assert.IsNotNull(multiBranchBuild);
            //Assert.IsNotNull(folderBuild);
            //Assert.IsNotNull(organizationFolderBuild);
                        
        }

        [TestMethod]
        public void RunTest()
        {
            JenkinsModelRun build;

            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                build = jenkins.RunJobComplete("Freestyle Test Pure");
            }

            //Assert.AreEqual(JenkinsResult.Success, build.Result, "build.Result");
        }

        [TestMethod]
        public void RunParamTest()
        {
            JenkinsModelRun build;
            using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            {
                JenkinsBuildParameters par = new JenkinsBuildParameters();
                par.Add("ParamA", "TestA");
                par.Add("ParamB", "TestB");
                par.Add("ParamC", "TestC");
                par.Add("CheckD", false);
                par.Add("CheckE", true);
                par.Add("TextBoxF", "TextF1\\nTextF2\\nTextF3");

                build = jenkins.RunJobComplete("Freestyle Test Parameter", par);
            }

            //Assert.AreEqual(JenkinsResult.Success, build.Result, "build.Result");
        }

        [TestMethod]
        public void RunFileTest()
        {
            //JenkinsBuild build;
            //using (Jenkins jenkins = new Jenkins(this.host, this.login, this.password))
            //{
            //    using (FileStream file = File.OpenRead(filePath))
            //    {
            //        JenkinsBuildParameters par = new JenkinsBuildParameters();
            //        par.Add("AppStartATF.core.gz", file, filePath);
            //        par.Add("NavVersion", "");
            //        par.Add("CoreDescription", "");
            //        build = jenkins.RunJobComplete(jobName, par);
            //    }
            //}

            //Assert.AreEqual(JenkinsBuildResult.Success, build.Result, "build.Result");
            //Assert.AreNotEqual(0, build.Number, "Number");
        }

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
