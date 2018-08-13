pipeline {
  agent {
    node {
        label 'aks'
    }
  }

  stages {
    stage('Deploy') {
      azureFunctionAppPublish azureCredentialsId: 'jerome-azure-personal',
                              resourceGroup: 'acostaslscicd', appName: 'acostaslscicd',
                              filePath: '**/*.csx,**/*.json'
    }
  }
}
