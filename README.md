# Scheduling Web Mobile Api

## How to use?
It is need have Docker installed

**Steps:**

• Clone the repository

`git clone git@github.com:Infnet-SJ90/scheduling-web-mobile-api.git`

• Open path of project

`cd scheduling-web-mobile-api`

• Build docker image

`docker build -t sj90image .`

• Run application

`docker run -d -p 8080:80 -it sj90image --name sj90webapi`

**Endpoint:**: http://localhost:8080/