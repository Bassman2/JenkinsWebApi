set JENKINSURL=http://tiny:8080
set JENKINSUSER=Tester:tester 

curl --user %JENKINSUSER% -o hudson.xsd %JENKINSURL%/api/schema

curl --user %JENKINSUSER% -o defaultCrumbIssuer.xsd %JENKINSURL%/crumbIssuer/api/schema
curl --user %JENKINSUSER% -o localPluginManager.xsd %JENKINSURL%/pluginManager/api/schema
curl --user %JENKINSUSER% -o rootActionImpl.xsd %JENKINSURL%/credentials/api/schema


rem user
curl --user %JENKINSUSER% -o asyncPeople.xsd %JENKINSURL%/asynchPeople/api/schema
curl --user %JENKINSUSER% -o people.xsd %JENKINSURL%/people/api/schema
curl --user %JENKINSUSER% -o user.xsd %JENKINSURL%/user/Tester/api/schema


rem views
curl --user %JENKINSUSER% -o categorizedJobsView.xsd  %JENKINSURL%/view/Categorization/api/schema
curl --user %JENKINSUSER% -o listView.xsd %JENKINSURL%/view/Test/api/schema
curl --user %JENKINSUSER% -o myView.xsd %JENKINSURL%/view/my%%20view/api/schema
curl --user %JENKINSUSER% -o dashboard.xsd %JENKINSURL%/view/Dashboard/api/schema
curl --user %JENKINSUSER% -o allView.xsd %JENKINSURL%/view/all/api/schema
curl --user %JENKINSUSER% -o multiJobView.xsd %JENKINSURL%/view/MultiJob/api/schema

rem jobs & builds
curl --user %JENKINSUSER% -o externalJob.xsd %JENKINSURL%/job/External%%20Job/api/schema

curl --user %JENKINSUSER% -o folder.xsd %JENKINSURL%/job/Folder/api/schema

curl --user %JENKINSUSER% -o freeStyleProject.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/api/schema
curl --user %JENKINSUSER% -o freeStyleBuild.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/2/api/schema

curl --user %JENKINSUSER% -o organizationFolder.xsd %JENKINSURL%/job/GitHub/api/schema

curl --user %JENKINSUSER% -o workflowMultiBranchProject.xsd %JENKINSURL%/job/Multibranch%%20Pipeline/api/schema

curl --user %JENKINSUSER% -o matrixProject.xsd %JENKINSURL%/job/Multiconfiguration/api/schema
curl --user %JENKINSUSER% -o matrixBuild.xsd %JENKINSURL%/job/Multiconfiguration/lastSuccessfulBuild/api/schema

curl --user %JENKINSUSER% -o workflowJob.xsd %JENKINSURL%/job/Pipeline/api/schema
curl --user %JENKINSUSER% -o workflowRun.xsd %JENKINSURL%/job/Pipeline/1/api/schema

curl --user %JENKINSUSER% -o multijobJob.xsd %JENKINSURL%/job/MultiJob/api/schema
curl --user %JENKINSUSER% -o multijobRun.xsd %JENKINSURL%/job/MultiJob/1/api/schema

rem test result
curl --user %JENKINSUSER% -o testResult.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/9/testReport/api/schema
curl --user %JENKINSUSER% -o caseResult.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/xml/_empty_/api/schema
curl --user %JENKINSUSER% -o packageResult.xsd %JENKINSURL%/job/Freestyle%%20Test%%20Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/api/schema

rem new new new 
curl --user %JENKINSUSER% -o computerSet.xsd %JENKINSURL%/computer/api/schema
curl --user %JENKINSUSER% -o masterComputer.xsd %JENKINSURL%/computer/(master)/api/schema
curl --user %JENKINSUSER% -o slaveComputer.xsd %JENKINSURL%/computer/Helper/api/schema
curl --user %JENKINSUSER% -o label.xsd %JENKINSURL%/label/XXX_LABEL/api/schema
curl --user %JENKINSUSER% -o userFacingAction.xsd %JENKINSURL%/credentials/store/system/api/schema
curl --user %JENKINSUSER% -o queue.xsd %JENKINSURL%/queue/api/schema

rem plugins
curl --user %JENKINSUSER% -o injectedEnvVars.xsd %JENKINSURL%/job/Downstream_A_Test/2/injectedEnvVars/api/schema
curl --user %JENKINSUSER% -o buildGraph.xsd %JENKINSURL%/job/Downstream_A_Test/2/BuildGraph/api/schema
pause
