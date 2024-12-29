BACKEND_PATH = 'backend/'
MOBILE_PATH = 'mobile/'
ZEUS_API_WEB_PATH = "${BACKEND_PATH}Zeus.Api.Web/"
ZEUS_API_GRPC_PATH = "${BACKEND_PATH}Zeus.Api.gRPC/"
ZEUS_DAEMON_RUNNER_PATH = "${BACKEND_PATH}Zeus.Api.gRPC/"

pipeline {
    agent any
    environment {
        MIRROR_URL = 'git@github.com:EpitechPromo2027/B-DEV-500-NAN-5-2-area-matheo.coquet.git'
    }
    stages {
        stage ('Build') {
            parallel {
                stage('Zeus Api Web') {
                    steps {
                        script {
                            def ZEUS_API_WEB_IMAGE_TEST = "zeus-api-web-test:${env.BUILD_ID}"
                            sh "docker build -f ${ZEUS_API_WEB_PATH}/Dockerfile -t ${ZEUS_API_WEB_IMAGE_TEST} ${BACKEND_PATH} --no-cache"
                        }
                    }
                }
                stage('Zeus Api gRPC') {
                    steps {
                        script {
                            def ZEUS_API_GRPC_IMAGE_TEST = "zeus-api-grpc-test:${env.BUILD_ID}"
                            sh "docker build -f ${ZEUS_API_GRPC_PATH}/Dockerfile -t ${ZEUS_API_GRPC_IMAGE_TEST} ${BACKEND_PATH} --no-cache"
                        }
                    }
                }
                stage('Zeus Daemon Runner') {
                    steps {
                        script {
                            def ZEUS_DAEMON_RUNNER_IMAGE_TEST = "zeus-daemon-runner-test:${env.BUILD_ID}"
                            sh "docker build -f ${ZEUS_DAEMON_RUNNER_PATH}/Dockerfile -t ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} ${BACKEND_PATH} --no-cache"
                        }
                    }
                }
                stage('Mobile App') {
                    steps {
                        script {
                            sh "docker build -f ${MOBILE_PATH}/Dockerfile.base -t mobile-base ${MOBILE_PATH}"
                            def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_ID}"
                            sh "docker build -f ${MOBILE_PATH}/Dockerfile -t ${MOBILE_IMAGE_TEST} ${MOBILE_PATH} --no-cache"
                        }
                    }
                }
            }
        }

        stage('Test') {
            parallel {
                stage('Mobile App') {
                    steps {
                        script {
                            def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_ID}"
                            sh "docker run --rm ${MOBILE_IMAGE_TEST}"
                        }
                    }
                }
            }
        }

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
    post {
        always {
            script {
                def ZEUS_API_WEB_IMAGE_TEST = "zeus-api-web-test:${env.BUILD_ID}"
                def ZEUS_API_GRPC_IMAGE_TEST = "zeus-api-grpc-test:${env.BUILD_ID}"
                def ZEUS_DAEMON_RUNNER_IMAGE_TEST = "zeus-daemon-runner-test:${env.BUILD_ID}"
                def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_ID}"

                sh "docker rmi ${ZEUS_API_WEB_IMAGE_TEST} ${ZEUS_API_GRPC_IMAGE_TEST} ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} ${MOBILE_IMAGE_TEST} || true"
            }
        }
    }
}
