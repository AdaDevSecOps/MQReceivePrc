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
                    bat 'docker container create --name mqreceiveprc-master mqreceiveprc:5.20002.3.03'
                    bat 'docker container create --name mqreceiveprc-sale mqreceiveprc:5.20002.3.03'
                    bat 'docker container create --name mqreceiveprc-doc mqreceiveprc:5.20002.3.03'
                }
            }
        }
        stage('Copy file')
        {
            steps
            {
                echo 'Copy file...'
                script
                {
                    bat 'docker cp ./Appsetting/Master/. mqreceiveprc-master:/app'
                    bat 'docker cp ./Appsetting/Sale/. mqreceiveprc-sale:/app'
                    bat 'docker cp ./Appsetting/Doc/. mqreceiveprc-doc:/app'
                }
            }
        }
        stage('Start container')
        {
            steps
            {
                echo 'Copy file...'
                script
                {
                    bat 'docker start mqreceiveprc-master'
                    bat 'docker start mqreceiveprc-sale'
                    bat 'docker start mqreceiveprc-doc'
                }
            }
        }
    }
}
