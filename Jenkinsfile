
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
    stage('Deploy') {
      steps {

        sh "rm -rf ./*"
        unstash name: 'builtSources'
        sh "ls ./azurefunctionscicd/bin/Release/netstandard2.0/*"
        sh "mv ./azurefunctionscicd/bin/Release/netstandard2.0/ ./deploy"
        sh "ls ./deploy"
        azureFunctionAppPublish azureCredentialsId: 'jerome-azure-personal',
                                resourceGroup: 'consplanuseast2', appName: 'consplanuseast2',
                                filePath: './deploy/**'

      }
    }
  }
}
