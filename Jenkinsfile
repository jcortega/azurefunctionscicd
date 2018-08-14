
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
        dir('./azurefunctionscicd/bin/Release/netstandard2.0/') {
          stash name: 'builtSources'
        }

      }
    }
    stage('Deploy') {
      steps {

        //sh "rm -rf ./*"
        sh "ls ./azurefunctionscicd/bin/Release/netstandard2.0/*"
        dir('builtSources') {
          unstash name: 'builtSources'
        }
        sh "ls -al builtSources"
        azureFunctionAppPublish azureCredentialsId: 'jerome-azure-personal',
                                resourceGroup: 'consplanuseast2', appName: 'consplanuseast2',
                                filePath: 'azurefunctionscicd/bin/Release/netstandard2.0/**/*'

      }
    }
  }
}
