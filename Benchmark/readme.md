# Benchmark

## Run `Benchmark` to compare Data Read operation
- In this Benchmark we are reading data with the sizes of 200,1024,2048,4096 in bytes
- move to directory `cd ..\Benchmark` 
- run `dotnet run -c Release -f net8.0`


##
# Results - Garner vs Redis (Sample)

## Read Operation on AMD Machine
![alt](Images/AMD/ReadOps.png)

## Write Operation on AMD Machine
![alt](Images/AMD/WriteOps.png)

## Read Operation on Intel Machine
![alt](Images/Intel/ReadOps.png)
