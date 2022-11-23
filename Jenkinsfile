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
        stage('Run container')
        {
            steps
            {
                echo 'Run container...'
                script
                {
                    bat 'docker run --name mqreceiveprc mqreceiveprc:5.20002.3.03'
                }
            }
        }
    }
}
