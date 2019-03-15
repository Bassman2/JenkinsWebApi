dl -u bs -p ente51 -o hudson.xsd http://localhost:8080/api/schema
dl -u bs -p ente51 -o defaultCrumbIssuer.xsd http://localhost:8080/crumbIssuer/api/schema
dl -u bs -p ente51 -o localPluginManager.xsd http://localhost:8080/pluginManager/api/schema
dl -u bs -p ente51 -o rootActionImpl.xsd http://localhost:8080/credentials/api/schema


rem user
dl -u bs -p ente51 -o asyncPeople.xsd http://localhost:8080/asynchPeople/api/schema
dl -u bs -p ente51 -o people.xsd http://localhost:8080/people/api/schema
dl -u bs -p ente51 -o user.xsd http://localhost:8080/user/bs/api/schema


rem views
dl -u bs -p ente51 -o categorizedJobsView.xsd  http://localhost:8080/view/Categorization/api/schema
dl -u bs -p ente51 -o listView.xsd http://localhost:8080/view/Test/api/schema
dl -u bs -p ente51 -o myView.xsd "http://localhost:8080/view/my view/api/schema"
dl -u bs -p ente51 -o dashboard.xsd "http://localhost:8080/view/Dashboard/api/schema"

rem jobs & builds
dl -u bs -p ente51 -o externalJob.xsd "http://localhost:8080/job/External Job/api/schema"

dl -u bs -p ente51 -o folder.xsd "http://localhost:8080/job/Folder/api/schema"

dl -u bs -p ente51 -o freeStyleProject.xsd "http://localhost:8080/job/Freestyle Test Doc/api/schema"
dl -u bs -p ente51 -o freeStyleBuild.xsd "http://localhost:8080/job/Freestyle Test Doc/2/api/schema"

dl -u bs -p ente51 -o organizationFolder.xsd http://localhost:8080/job/GitHub/api/schema

dl -u bs -p ente51 -o workflowMultiBranchProject.xsd "http://localhost:8080/job/Multibranch Pipeline/api/schema"

dl -u bs -p ente51 -o matrixProject.xsd http://localhost:8080/job/Multiconfiguration/api/schema
dl -u bs -p ente51 -o matrixBuild.xsd http://localhost:8080/job/Multiconfiguration/lastSuccessfulBuild/api/schema

dl -u bs -p ente51 -o workflowJob.xsd http://localhost:8080/job/Pipeline/api/schema
dl -u bs -p ente51 -o workflowRun.xsd http://localhost:8080/job/Pipeline/1/api/schema

dl -u bs -p ente51 -o multijobJob.xsd http://localhost:8080/job/MultiJob/api/schema
dl -u bs -p ente51 -o multijobRun.xsd http://localhost:8080/job/MultiJob/1/api/schema

rem test result
dl -u bs -p ente51 -o testResult.xsd "http://localhost:8080/job/Freestyle Test Doc/9/testReport/api/schema"
dl -u bs -p ente51 -o caseResult.xsd "http://localhost:8080/job/Freestyle Test Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/xml/_empty_/api/schema"
dl -u bs -p ente51 -o packageResult.xsd "http://localhost:8080/job/Freestyle Test Doc/lastCompletedBuild/testReport/UT_Av2UnitTest/api/schema"

rem new new new 
dl -u bs -p ente51 -o computerSet.xsd http://localhost:8080/computer/api/schema
dl -u bs -p ente51 -o masterComputer.xsd http://localhost:8080/computer/(master)/api/schema
dl -u bs -p ente51 -o slaveComputer.xsd http://localhost:8080/computer/b00014/api/schema
dl -u bs -p ente51 -o label.xsd http://localhost:8080/label/XXX_LABEL/api/schema

dl -u bs -p ente51 -o allView.xsd http://localhost:8080/view/all/api/schema
dl -u bs -p ente51 -o userFacingAction.xsd http://localhost:8080/credentials/store/system/api/schema


