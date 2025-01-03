BACKEND_PATH = 'backend/'
ZEUS_API_WEB_PATH = "${BACKEND_PATH}Zeus.Api.Web/"
ZEUS_API_GRPC_PATH = "${BACKEND_PATH}Zeus.Api.gRPC/"
ZEUS_DAEMON_RUNNER_PATH = "${BACKEND_PATH}Zeus.Api.gRPC/"

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
    hostPathVolume(mountPath: '/var/run/docker.sock', hostPath: '/var/run/docker.sock')
]) {

    node(POD_LABEL) {
        stage('Zeus Api Web') {
            container('docker') {
                stage('Test docker compilation') {
                    checkout scm

                    def ZEUS_API_WEB_IMAGE_TEST = "zeus-api-web-test:${env.BUILD_ID}"
                    sh "docker build -f ${ZEUS_API_WEB_PATH}/Dockerfile -t ${ZEUS_API_WEB_IMAGE_TEST} ${BACKEND_PATH} --no-cache"
                }
            }
        }
        stage('Zeus Api gRPC') {
            container('docker') {
                stage('Test docker compilation') {
                    checkout scm

                    def ZEUS_API_GRPC_IMAGE_TEST = "zeus-api-grpc-test:${env.BUILD_ID}"
                    sh "docker build -f ${ZEUS_API_GRPC_PATH}/Dockerfile -t ${ZEUS_API_GRPC_IMAGE_TEST} ${BACKEND_PATH} --no-cache"
                }
            }
        }
        stage('Zeus Daemon Runner') {
            container('docker') {
                stage('Test docker compilation') {
                    checkout scm

                    def ZEUS_DAEMON_RUNNER_IMAGE_TEST = "zeus-daemon-runner-test:${env.BUILD_ID}"
                    sh "docker build -f ${ZEUS_DAEMON_RUNNER_PATH}/Dockerfile -t ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} ${BACKEND_PATH} --no-cache"
                }
            }
        }

        stage('Mirror') {
            when {
                branch 'main'
            }
            container('git') {
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
}
