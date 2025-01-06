BACKEND_PATH = 'backend'
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
        stage('Backend Build') {
            container('docker') {
                stage('Container setup') {
                    checkout scm
                }
                stage('Zeus Api Web') {
                    def ZEUS_API_WEB_IMAGE_TEST = "zeus-api-web-test:${env.BUILD_ID}"
                    sh "docker build -f ${ZEUS_API_WEB_PATH}/Dockerfile -t ${ZEUS_API_WEB_IMAGE_TEST} ${BACKEND_PATH}"
                    sh "docker rmi ${ZEUS_API_WEB_IMAGE_TEST}"
                }
                stage('Zeus Api gRPC') {
                    def ZEUS_API_GRPC_IMAGE_TEST = "zeus-api-grpc-test:${env.BUILD_ID}"
                    sh "docker build -f ${ZEUS_API_GRPC_PATH}/Dockerfile -t ${ZEUS_API_GRPC_IMAGE_TEST} ${BACKEND_PATH}"
                    sh "docker rmi ${ZEUS_API_GRPC_IMAGE_TEST}"
                }
                stage('Zeus Daemon Runner') {
                    def ZEUS_DAEMON_RUNNER_IMAGE_TEST = "zeus-daemon-runner-test:${env.BUILD_ID}"
                    sh "docker build -f ${ZEUS_DAEMON_RUNNER_PATH}/Dockerfile -t ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} ${BACKEND_PATH}"
                    sh "docker rmi ${ZEUS_DAEMON_RUNNER_IMAGE_TEST}"
                }
            }
        }

        stage('Git actions') {
            container('git') {
                stage('Mirror push') {
                    checkout scm

                    def currentBranch = env.CHANGE_BRANCH

                    if (currentBranch == 'main') {
                        sh "git remote add mirror ${MIRROR_URL}"

                        sh "git checkout main"

                        withCredentials([sshUserPrivateKey(credentialsId: 'G-EPIJENKINS_SSH_KEY', keyFileVariable: 'PRIVATE_KEY')]) {
                            sh 'GIT_SSH_COMMAND="ssh -i $PRIVATE_KEY" git push --tags --force --prune mirror "refs/remotes/origin/*:refs/heads/*"'
                        }
                    } else {
                        echo "Not on main branch (${currentBranch}), skipping mirror push."
                    }
                }
            }
        }
    }
}
