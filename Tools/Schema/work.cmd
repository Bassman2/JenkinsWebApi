@echo off

set JENKINSURL=http://Notebook
set JENKINSUSER=Tester:tester 

rem -----------------------------------------------------------------------------------------------
rem system
rem -----------------------------------------------------------------------------------------------

curl --user %JENKINSUSER% -o systemHudson.xsd %JENKINSURL%/api/schema
curl --user %JENKINSUSER% -o systemCrumbIssuer.xsd %JENKINSURL%/crumbIssuer/api/schema
curl --user %JENKINSUSER% -o systemPluginManager.xsd %JENKINSURL%/pluginManager/api/schema
curl --user %JENKINSUSER% -o systemCredentials.xsd %JENKINSURL%/credentials/api/schema
curl --user %JENKINSUSER% -o systemCredentialsStore.xsd %JENKINSURL%/credentials/store/system/api/schema
curl --user %JENKINSUSER% -o systemQueue.xsd %JENKINSURL%/queue/api/schema

rem -----------------------------------------------------------------------------------------------
rem computer
rem -----------------------------------------------------------------------------------------------

curl --user %JENKINSUSER% -o computer.xsd %JENKINSURL%/computer/api/schema
curl --user %JENKINSUSER% -o computerMaster.xsd %JENKINSURL%/computer/(master)/api/schema
curl --user %JENKINSUSER% -o computerSlave.xsd %JENKINSURL%/computer/Helper/api/schema
curl --user %JENKINSUSER% -o computerLabel.xsd %JENKINSURL%/label/XXX_LABEL/api/schema

rem -----------------------------------------------------------------------------------------------
rem user
rem -----------------------------------------------------------------------------------------------

curl --user %JENKINSUSER% -o people.xsd %JENKINSURL%/people/api/schema
curl --user %JENKINSUSER% -o peopleAsync.xsd %JENKINSURL%/asynchPeople/api/schema
curl --user %JENKINSUSER% -o peopleUser.xsd %JENKINSURL%/user/Tester/api/schema

rem -----------------------------------------------------------------------------------------------
rem views
rem -----------------------------------------------------------------------------------------------

curl --user %JENKINSUSER% -o viewList.xsd %JENKINSURL%/view/ListView/api/schema
curl --user %JENKINSUSER% -o viewMy.xsd %JENKINSURL%/view/MyView/api/schema
curl --user %JENKINSUSER% -o viewAll.xsd %JENKINSURL%/view/all/api/schema
curl --user %JENKINSUSER% -o viewMyGlobal.xsd %JENKINSURL%/me/my-views/view/GlobalView/api/schema

curl --user %JENKINSUSER% -o viewCategorizedJobs.xsd  %JENKINSURL%/view/Categorization/api/schema
curl --user %JENKINSUSER% -o viewDashboard.xsd %JENKINSURL%/view/Dashboard/api/schema
curl --user %JENKINSUSER% -o viewMmultiJob.xsd %JENKINSURL%/view/MultiJob/api/schema

rem -----------------------------------------------------------------------------------------------
rem jobs & builds
rem -----------------------------------------------------------------------------------------------

:: Free Style
curl --user %JENKINSUSER% -o jobFreeStyle.xsd %JENKINSURL%/job/FreeStyle/api/schema
curl --user %JENKINSUSER% -o bldFreeStyle.xsd %JENKINSURL%/job/FreeStyle/1/api/schema

:: Maven-Project https://plugins.jenkins.io/maven-plugin
curl --user %JENKINSUSER% -o jobMaven.xsd %JENKINSURL%/job/Maven/api/schema
curl --user %JENKINSUSER% -o bldMaven.xsd %JENKINSURL%/job/Maven/1/api/schema

:: Pipeline https://plugins.jenkins.io/workflow-aggregato
curl --user %JENKINSUSER% -o jobPipeline.xsd %JENKINSURL%/job/Pipeline/api/schema
curl --user %JENKINSUSER% -o bldPipeline.xsd %JENKINSURL%/job/Pipeline/1/api/schema

:: Multiconfiguration
curl --user %JENKINSUSER% -o jobMulticonfiguration.xsd %JENKINSURL%/job/Multiconfiguration/api/schema
curl --user %JENKINSUSER% -o bldMulticonfiguration.xsd %JENKINSURL%/job/Multiconfiguration/1/api/schema

:: MultiJob
curl --user %JENKINSUSER% -o jobMulti.xsd %JENKINSURL%/job/MultiJob/api/schema
curl --user %JENKINSUSER% -o bldMulti.xsd %JENKINSURL%/job/MultiJob/1/api/schema

rem -----------------------------------------------------------------------------------------------
rem jobs without builds
rem -----------------------------------------------------------------------------------------------

:: Folder
curl --user %JENKINSUSER% -o jobFolder.xsd %JENKINSURL%/job/Folder/api/schema

:: External Job
curl --user %JENKINSUSER% -o jobExternal.xsd %JENKINSURL%/job/ExternalJob/api/schema

:: GitHubOrganization
curl --user %JENKINSUSER% -o jobGitHubOrganizationFolder.xsd %JENKINSURL%/job/GitHubOrganization/api/schema

:: Multibranch Pipeline
curl --user %JENKINSUSER% -o jobMultibranchPipeline.xsd %JENKINSURL%/job/MultibranchPipeline/api/schema

rem -----------------------------------------------------------------------------------------------
rem test results
rem -----------------------------------------------------------------------------------------------

curl --user %JENKINSUSER% -o testResult.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/9/testReport/api/schema
curl --user %JENKINSUSER% -o testCaseResult.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/xml/_empty_/api/schema
curl --user %JENKINSUSER% -o testPackageResult.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/api/schema

rem -----------------------------------------------------------------------------------------------
rem plugins
rem -----------------------------------------------------------------------------------------------

curl --user %JENKINSUSER% -o pluginInjectedEnvVars.xsd %JENKINSURL%/job/Downstream_A_Test/2/injectedEnvVars/api/schema
curl --user %JENKINSUSER% -o pluginBuildGraph.xsd %JENKINSURL%/job/Downstream_A_Test/2/BuildGraph/api/schema

pause
