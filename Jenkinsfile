
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
        dir("./azurefunctionscicd.test") {
          sh "dotnet test"
        }
      }
    }
    stage('Deploy') {
      steps {

        //sh "rm -rf ./*"
        sh "ls ./azurefunctionscicd/bin/Release/netstandard2.0/*"
        unstash name: 'builtSources'
        dir('azurefunctionscicd/bin/Release/netstandard2.0/') {
          azureFunctionAppPublish azureCredentialsId: 'jerome-azure-personal',
                                  resourceGroup: 'consplanuseast2', appName: 'consplanuseast2',
                                  filePath: '**/*'
        }

      }
    }
  }
}
