
pipeline {
  agent {
    node {
        label 'aks'
    }
  }

  stages {
    stage('Build') {
      steps {

        sh "nuget restore"
        sh "msbuild /t:Build /p:Configuration=Release"
        stash name: 'builtSources'
      }
    }
    stage('Unit Test') {
      agent {
        docker {
            image 'microsoft/dotnet'
        }
      }
      steps {
        sh "cd ./azurefunctionscicd.test && dotnet test"
      }
    }
    stage('Deploy Test Environment') {
      agent {
        docker {
            image 'microsoft/azure-cli'
        }
      }
      steps {
        withCredentials([azureServicePrincipal('jerome-azure-personal')]) {
            sh 'az login --service-principal -u $AZURE_CLIENT_ID -p $AZURE_CLIENT_SECRET --tenant $AZURE_TENANT_ID'
            sh 'az group create --name functions-branch-${GIT_BRANCH}-build-${BUILD_ID} --location "East US 2"'
            sh 'az group deployment create  --name functions-branch-${GIT_BRANCH}-build-${BUILD_ID} --resource-group functions-branch-${GIT_BRANCH}-build-${BUILD_ID} --template-file ./infrastructure/template.json'
        }

      }
    }
    stage('Deploy Function') {
      steps {
        echo "Wait 60 seconds before deploying function..."
        sh "sleep 60"
        unstash name: 'builtSources'
        dir('azurefunctionscicd/bin/Release/netstandard2.0/') {
          azureFunctionAppPublish azureCredentialsId: 'jerome-azure-personal',
                                  resourceGroup: "functions-branch-${GIT_BRANCH}-build-${BUILD_ID}", appName: 'consplanuseast2',
                                  filePath: '**/*'
        }
      }
    }
    stage('Integration Test') {
      steps {
        echo "Integration testing..."
        sh "sleep 60"
        sh "curl -sf https://consplanuseast2.azurewebsites.net/api/HttpTrigger?name=jenkins"
      }
    }
    stage('Destroy Test Environment') {
      agent {
        docker {
            image 'microsoft/azure-cli'
        }
      }
       steps {
         withCredentials([azureServicePrincipal('jerome-azure-personal')]) {
           echo "Destroying test environment..."
           sh 'az login --service-principal -u $AZURE_CLIENT_ID -p $AZURE_CLIENT_SECRET --tenant $AZURE_TENANT_ID'
           sh 'az group delete --name functions-branch-${GIT_BRANCH}-build-${BUILD_ID} --yes'
         }
       }
    }
  }
}
