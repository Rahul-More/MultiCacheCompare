## we are comparing read operation from muliple cache application redis garnet & scaleout

## Run Redis locally

- Docker pull Image of redis  <br> ` docker pull redis ` && ` docker image ls ` 

- Run Redis locally <br>
  - Docker RUN ` docker run -p 7379:6379 --name my-local-redis -d redis `
  - Verify docker containers List ` docker ps `

## Run Garnet locally

- Docker pull Image of redis  <br> ` docker pull ghcr.io/microsoft/garnet ` && ` docker image ls ` 

- Run Redis locally <br>
  - Docker RUN ` docker run -p 6379:6379 --name local-garnet -d ghcr.io/microsoft/garnet `
  - Verify docker containers List ` docker ps `

