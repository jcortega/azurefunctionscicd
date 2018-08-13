pipeline {
  agent {
    node {
        label 'aks'
    }
  }

  stages {
    stage('Deploy') {
      steps {
        azureFunctionAppPublish azureCredentialsId: 'jerome-azure-personal',
                                resourceGroup: 'acostaslscicd', appName: 'acostaslscicd',
                                filePath: '**/*.csx,**/*.json'
      }
    }
  }
}
