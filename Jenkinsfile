
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
        sh "ls ./azurefunctionscicd/bin/Release/netstandard2.0"
      }
    }
  }
}
