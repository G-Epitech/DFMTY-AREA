BACKEND_PATH = 'backend'
MOBILE_PATH = 'mobile'
WEB_PATH = 'web'
ZEUS_API_WEB_PATH = "${BACKEND_PATH}/Zeus.Api.Presentation.Web"
ZEUS_API_GRPC_PATH = "${BACKEND_PATH}/Zeus.Api.Presentation.gRPC"
ZEUS_DAEMON_RUNNER_PATH = "${BACKEND_PATH}/Zeus.Daemon.Runner"
MIRROR_URL = 'git@github.com:EpitechPromo2027/B-DEV-500-NAN-5-2-area-matheo.coquet.git'

podTemplate(containers: [
    containerTemplate(
        name: 'docker',
        image: 'docker',
        command: 'sleep',
        args: '1h',
        resourceRequestCpu: '500m',
        resourceLimitCpu: '1',
        resourceRequestMemory: '700Mi',
        resourceLimitMemory: '1Gi'
    ),
    containerTemplate(
        name: 'git',
        image: 'alpine/git',
        command: 'sleep',
        args: '1h',
        resourceRequestCpu: '300m',
        resourceLimitCpu: '600m',
        resourceRequestMemory: '300Mi',
        resourceLimitMemory: '600Mi'
    ),
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
                        def ZEUS_API_WEB_IMAGE_TEST = "zeus-api-web-test:${env.BUILD_TAG}"
                        sh "docker build -f ${ZEUS_API_WEB_PATH}/Dockerfile -t ${ZEUS_API_WEB_IMAGE_TEST} ${BACKEND_PATH}"
                        sh "docker rmi ${ZEUS_API_WEB_IMAGE_TEST} || true"
                    }
                },
                'Zeus Api gRPC': {
                    container('docker') {
                        def ZEUS_API_GRPC_IMAGE_TEST = "zeus-api-grpc-test:${env.BUILD_TAG}"
                        sh "docker build -f ${ZEUS_API_GRPC_PATH}/Dockerfile -t ${ZEUS_API_GRPC_IMAGE_TEST} ${BACKEND_PATH}"
                        sh "docker rmi ${ZEUS_API_GRPC_IMAGE_TEST} || true"
                    }
                },
                'Zeus Daemon Runner': {
                    container('docker') {
                        def ZEUS_DAEMON_RUNNER_IMAGE_TEST = "zeus-daemon-runner-test:${env.BUILD_TAG}"
                        sh "docker build -f ${ZEUS_DAEMON_RUNNER_PATH}/Dockerfile -t ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} ${BACKEND_PATH}"
                        sh "docker rmi ${ZEUS_DAEMON_RUNNER_IMAGE_TEST} || true"
                    }
                }
            )
        }

        stage('Mobile Build') {
            container('docker') {
                sh "docker build -f ${MOBILE_PATH}/Dockerfile.base -t mobile-base ${MOBILE_PATH}"
                def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_TAG}"
                sh "docker build -f ${MOBILE_PATH}/Dockerfile -t ${MOBILE_IMAGE_TEST} ${MOBILE_PATH} --no-cache"
            }
        }

        stage('Mobile Test') {
            container('docker') {
                def MOBILE_IMAGE_TEST = "mobile-test:${env.BUILD_TAG}"
                def runStatus = sh(script: "docker run --rm ${MOBILE_IMAGE_TEST}", returnStatus: true)
                sh "docker rmi ${MOBILE_IMAGE_TEST} || true"
                if (runStatus != 0) {
                    error 'Docker run failed for Mobile App'
                }
            }
        }

        stage('Web Prepare') {
            container('docker') {
                def WEB_IMAGE_TEST = "web-test:${env.BUILD_TAG}"
                sh "docker build -f ${WEB_PATH}/Dockerfile -t ${WEB_IMAGE_TEST} ${WEB_PATH}"
                sh "docker stop angular-container_${env.BUILD_TAG} || true"
                sh "docker rm angular-container_${env.BUILD_TAG} || true"
                sh "docker run -d --name angular-container_${env.BUILD_TAG} ${WEB_IMAGE_TEST} sleep infinity"
            }
        }

        stage('Web Build') {
            container('docker') {
                shInContainer("angular-container_${env.BUILD_TAG}", "web-test:${env.BUILD_TAG}", 'npm run build --configuration=test')
            }
        }

        stage('Web Lint') {
            container('docker') {
                shInContainer("angular-container_${env.BUILD_TAG}", "web-test:${env.BUILD_TAG}", 'npm run lint')
            }
        }

        stage('Web Format') {
            container('docker') {
                shInContainer("angular-container_${env.BUILD_TAG}", "web-test:${env.BUILD_TAG}", 'npm run format:check')
            }
        }

        stage('Web Cleanup') {
            container('docker') {
                sh "docker stop angular-container_${env.BUILD_TAG}"
                sh "docker rm angular-container_${env.BUILD_TAG}"
                sh "docker rmi web-test:${env.BUILD_TAG} || true"
            }
        }

        stage('Git Mirror') {
            container('git') {
                checkout scm

                def currentBranch = env.BRANCH_NAME

                if (currentBranch == 'main') {
                    sh "git config --global --add safe.directory ${WORKSPACE}"

                    sh "git remote add mirror ${MIRROR_URL}"

                    sh 'git checkout main'

                    withCredentials([sshUserPrivateKey(credentialsId: 'G-EPIJENKINS_SSH_KEY', keyFileVariable: 'PRIVATE_KEY')]) {
                        sh 'GIT_SSH_COMMAND="ssh -i $PRIVATE_KEY" git push --tags --force --prune mirror "refs/remotes/origin/*:refs/heads/*"'
                    }
                } else {
                    echo 'Not on main branch, skipping mirror push.'
                }
            }
        }
    }
}

def shInContainer(String containerName, String imageName, String command) {
    def returnStatus = sh(script: "docker exec ${containerName} ${command}", returnStatus: true)
    if (returnStatus != 0) {
        sh "docker stop ${containerName}"
        sh "docker rm ${containerName}"
        sh "docker rmi ${imageName} || true"
        error "Command failed in container ${containerName}: ${command}"
    }
}
