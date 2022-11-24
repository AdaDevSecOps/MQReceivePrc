def githubRepo = 'https://github.com/AdaDevSecOps/MQReceivePrc.git'
def githubBranch = 'main'

pipeline
{
    agent any
    environment
    {
        imagename = "mqreceiveprc:5.20002.3.03"
        dockerImage = ''
    }
    stages{
        stage("Git Clone")
        {
            steps
            {
                echo "========Cloning Git========"
                git url: githubRepo,
                    branch: githubBranch
            }
            post
            {
                success
                {
                    echo "========Cloning Git successfully========"
                }
                failure
                {
                    echo "========Cloning Git failed========"
                }
            }
        }
        stage('Build Image')
        {
            steps
            {
                echo 'Building...'
                script
                {
                    dockerImage = docker.build imagename
                }
            }
        }
        stage('Delete container')
        {
            steps
            {
                echo 'Delete container...'
                script
                {
                    bat 'docker rm -f mqreceiveprc'
                    bat 'docker rm -f mqreceiveprc-master'
                    bat 'docker rm -f mqreceiveprc-sale'
                    bat 'docker rm -f mqreceiveprc-doc'
                }
            }
        }
    }
}
