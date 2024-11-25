pipeline {
    agent any
    environment {
        MIRROR_URL = 'git@github.com:EpitechPromo2027/B-DEV-500-NAN-5-2-area-matheo.coquet.git'
    }
    stages {
        stage ('Mirror') {
            when {
                branch 'main'
            }
            steps {
                checkout scm
                script {
                    if (sh(script: "git remote | grep mirror", returnStatus: true) == 0) {
                        sh "git remote remove mirror"
                    }
                    sh "git remote add mirror ${MIRROR_URL}"

                    sh "git checkout main"

                    withCredentials([sshUserPrivateKey(credentialsId: 'G-EPIJENKINS_SSH_KEY', keyFileVariable: 'PRIVATE_KEY')]) {
                        sh 'GIT_SSH_COMMAND="ssh -i $PRIVATE_KEY" git push --tags --force --prune mirror "refs/remotes/origin/*:refs/heads/*"'
                    }
                }
            }
        }
    }
}