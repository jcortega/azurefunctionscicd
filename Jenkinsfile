
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
        sh "ls azurefunctionscicd/bin/*"
        sh "ls azurefunctionscicd/bin/**/*"
        stash name: 'builtSources'
      }
    }
    stage('Deploy') {
      steps {
        //unstash name: 'builtSources'
        sh "rm rf ./*"
        sh "ls ./azurefunctionscicd/bin/Debug/netstandard2.0"
      }
    }
  }
}
