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
                                resourceGroup: 'consplanuseast2', appName: 'consplanuseast2',
                                filePath: '**/*.csx,**/*.json'
      }
    }
  }
}
