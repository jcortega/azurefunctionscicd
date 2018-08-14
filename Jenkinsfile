
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
        sh "msbuild"
        stash includes: 'azurefunctionscicd/bin/*', name: 'builtSources'

      }
    }
  }
}
