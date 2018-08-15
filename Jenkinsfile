
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
      steps {
        echo "Deploying test environment..."
      }
    }
    stage('Deploy Function') {
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
    stage('Integration Test') {
      steps {
        echo "Integration testing..."
      }
    }
    stage('Destroy Test Environment') {
      steps {
        echo "Destroying test environment..."
      }
    }
  }
}
