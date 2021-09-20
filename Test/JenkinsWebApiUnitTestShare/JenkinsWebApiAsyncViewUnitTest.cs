using JenkinsWebApi;
using JenkinsWebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsTest
{
    [TestClass]
    public class JenkinsWebApiAsyncViewUnitTest : JenkinsWebApiBaseUnitTest
    {
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
    }
}
