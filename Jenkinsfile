BACKEND_PATH = 'backend'
MOBILE_PATH = 'mobile'
ZEUS_API_WEB_PATH = "${BACKEND_PATH}/Zeus.Api.Presentation.Web"
ZEUS_API_GRPC_PATH = "${BACKEND_PATH}/Zeus.Api.Presentation.gRPC"
ZEUS_DAEMON_RUNNER_PATH = "${BACKEND_PATH}/Zeus.Daemon.Runner"

podTemplate(containers: [
    containerTemplate(
        name: 'docker',
        image: 'docker',
        command: 'sleep',
        args: '1h'
    ),
    containerTemplate(
        name: 'git',
        image: 'alpine/git',
        command: 'sleep',
        args: '1h'
    )
], volumes: [
    hostPathVolume(mountPath: '/var/run/docker.sock', hostPath: '/var/run/docker.sock'),
]) {
    node(POD_LABEL) {

        container('docker') {
            stage('Checkout') {
                checkout scm
            }
        }

        stage('Backend Build') {
            parallel(
                'Zeus Api Web': {
                    container('docker') {
                        def ZEUS_API_WEB_IMAGE_TEST = "zeus-api-web-test:${env.BUILD_ID}"
                        sh "docker build -f ${ZEUS_API_WEB_PATH}/Dockerfile -t ${ZEUS_API_WEB_IMAGE_TEST} ${BACKEND_PATH}"
                        sh "docker rmi ${ZEUS_API_WEB_IMAGE_TEST}"
                    }
                },
                'Zeus Api gRPC': {
                    container('docker') {
                        def ZEUS_API_GRPC_IMAGE_TEST = "zeus-api-grpc-test:${env.BUILD_ID}"
                        sh "docker build -f ${ZEUS_API_GRPC_PATH}/Dockerfile -t ${ZEUS_API_GRPC_IMAGE_TEST} ${BACKEND_PATH}"
                        sh "docker rmi ${ZEUS_API_GRPC_IMAGE_TEST}"
                    }
                },
                'Zeus Daemon Runner': {
                    container('docker') {
                        def ZEUS_DAEMON_RUNNER_IMAGE_TEST = "zeus-daemon-runner-test:${env.BUILD_ID}"
                        sh "docker build -f ${ZEUS_DAEMON_RUNNER_PATH}/Dockerfile -t ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} ${BACKEND_PATH}"
                        sh "docker rmi ${ZEUS_DAEMON_RUNNER_IMAGE_TEST}"
                    }
                }
            )
        }

        stage('Mobile Build') {
            container('docker') {
                sh "docker build -f ${MOBILE_PATH}/Dockerfile.base -t mobile-base ${MOBILE_PATH}"
                def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_ID}"
                sh "docker build -f ${MOBILE_PATH}/Dockerfile -t ${MOBILE_IMAGE_TEST} ${MOBILE_PATH}"
            }
        }

        stage('Mobile Test') {
            container('docker') {
                def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_ID}"
                def runStatus = sh(script: "docker run --rm ${MOBILE_IMAGE_TEST}", returnStatus: true)
                sh "docker rmi ${MOBILE_IMAGE_TEST}"
                if (runStatus != 0) {
                    error "Docker run failed for Mobile App"
                }
            }
        }

        stage('Git Mirror') {
            container('git') {
                checkout scm

                def currentBranch = env.BRANCH_NAME

                if (currentBranch == 'main') {
                    sh "git remote add mirror ${MIRROR_URL}"

                    sh "git checkout main"

                    withCredentials([sshUserPrivateKey(credentialsId: 'G-EPIJENKINS_SSH_KEY', keyFileVariable: 'PRIVATE_KEY')]) {
                        sh 'GIT_SSH_COMMAND="ssh -i $PRIVATE_KEY" git push --tags --force --prune mirror "refs/remotes/origin/*:refs/heads/*"'
                    }
                } else {
                    echo "Not on main branch, skipping mirror push."
                }
            }
        }
    }
}
