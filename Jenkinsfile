
pipeline {
  agent {
    node {
        label 'aks'
    }
  }

  stages {
    stage('Build') {
      steps {

        echo "Hellow world"
        sh "ls"
        sh "pwd"
        sh "nuget restore"
        sh "msbuild"
      }
    }
  }
}
