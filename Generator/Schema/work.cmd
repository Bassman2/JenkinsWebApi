dl -u dl -p dl -o hudson.xsd http://localhost:8080/api/schema
dl -u dl -p dl -o defaultCrumbIssuer.xsd http://localhost:8080/crumbIssuer/api/schema
dl -u dl -p dl -o localPluginManager.xsd http://localhost:8080/pluginManager/api/schema
dl -u dl -p dl -o rootActionImpl.xsd http://localhost:8080/credentials/api/schema


rem user
dl -u dl -p dl -o asyncPeople.xsd http://localhost:8080/asynchPeople/api/schema
dl -u dl -p dl -o people.xsd http://localhost:8080/people/api/schema
dl -u dl -p dl -o user.xsd http://localhost:8080/user/bs/api/schema


rem views
dl -u dl -p dl -o categorizedJobsView.xsd  http://localhost:8080/view/Categorization/api/schema
dl -u dl -p dl -o listView.xsd http://localhost:8080/view/Test/api/schema
dl -u dl -p dl -o myView.xsd "http://localhost:8080/view/my view/api/schema"
dl -u dl -p dl -o dashboard.xsd "http://localhost:8080/view/Dashboard/api/schema"
dl -u dl -p dl -o allView.xsd "http://localhost:8080/view/all/api/schema"

rem jobs & builds
dl -u dl -p dl -o externalJob.xsd "http://localhost:8080/job/External Job/api/schema"

dl -u dl -p dl -o folder.xsd "http://localhost:8080/job/Folder/api/schema"

dl -u dl -p dl -o freeStyleProject.xsd "http://localhost:8080/job/Freestyle Test Doc/api/schema"
dl -u dl -p dl -o freeStyleBuild.xsd "http://localhost:8080/job/Freestyle Test Doc/2/api/schema"

dl -u dl -p dl -o organizationFolder.xsd http://localhost:8080/job/GitHub/api/schema

dl -u dl -p dl -o workflowMultiBranchProject.xsd "http://localhost:8080/job/Multibranch Pipeline/api/schema"

dl -u dl -p dl -o matrixProject.xsd http://localhost:8080/job/Multiconfiguration/api/schema
dl -u dl -p dl -o matrixBuild.xsd http://localhost:8080/job/Multiconfiguration/lastSuccessfulBuild/api/schema

dl -u dl -p dl -o workflowJob.xsd http://localhost:8080/job/Pipeline/api/schema
dl -u dl -p dl -o workflowRun.xsd http://localhost:8080/job/Pipeline/1/api/schema

dl -u dl -p dl -o multijobJob.xsd http://localhost:8080/job/MultiJob/api/schema
dl -u dl -p dl -o multijobRun.xsd http://localhost:8080/job/MultiJob/1/api/schema

rem test result
dl -u dl -p dl -o testResult.xsd "http://localhost:8080/job/Freestyle Test Doc/9/testReport/api/schema"
dl -u dl -p dl -o caseResult.xsd "http://localhost:8080/job/Freestyle Test Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/xml/_empty_/api/schema"
dl -u dl -p dl -o packageResult.xsd "http://localhost:8080/job/Freestyle Test Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/api/schema"

rem new new new 
dl -u dl -p dl -o computerSet.xsd http://localhost:8080/computer/api/schema
dl -u dl -p dl -o masterComputer.xsd http://localhost:8080/computer/(master)/api/schema
dl -u dl -p dl -o slaveComputer.xsd http://localhost:8080/computer/b00014/api/schema
dl -u dl -p dl -o label.xsd http://localhost:8080/label/XXX_LABEL/api/schema
dl -u dl -p dl -o userFacingAction.xsd http://localhost:8080/credentials/store/system/api/schema
dl -u dl -p dl -o queue.xsd http://localhost:8080/queue/api/schema

rem plugins
dl -u dl -p dl -o injectedEnvVars.xsd http://localhost:8080/job/Downstream_A_Test/2/injectedEnvVars/api/schema
dl -u dl -p dl -o buildGraph.xsd http://localhost:8080/job/Downstream_A_Test/2/BuildGraph/api/schema
pause
