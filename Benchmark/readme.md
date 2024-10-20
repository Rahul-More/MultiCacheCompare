# Benchmark

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


## Run `MultiCacheCompare` to insert data into caches
- move to directory `cd .\MutliCacheCompare\` 
- run `dotnet restore`
- run `dotnet build` or `dotnet build -c Release` for release
- run `dotnet run` or `dotnet run -c Release -f net8.0` for release

## Run `Benchmark` to compare Data Read operation
- In this Benchmark we are reading data with the sizes of 200,1024,2048,4096 in bytes
- move to directory `cd ..\Benchmark` 
- run `dotnet run -c Release -f net8.0`


##
# Results - Garner vs Redis

![alt](Images/Screenshot%202024-10-20%20173024.png)
