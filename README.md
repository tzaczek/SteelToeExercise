# **SteelToe Exercise**

Repository with microservices learning project. The main goal behind this project is to learn SteelToe framework and practise microservices implemenetion.

## The business goal of the project

The main goal is to expose REST API which allows for GitHub reposiotors search. It should be possible to load starred repositores and search for repositories which use spsecifed license name. Authentication is currently very simplified and personal GitHub access token is required to process request.

## Architecture

![Blank diagram (14)](https://user-images.githubusercontent.com/47285958/153775029-ca328a1e-78f6-495f-a29b-25466d4a945e.png)

## Solution description - main projects

### GitHub.Repository.Analyzer.GitHub.Client
REST API exposing endpint for GitHubRepositores Search. It is also reposible for orchstrating calls to the Loader and Processor.

### GitHub.Repository.Analyzer.Loader
Loader of GitHub repositores.

### GitHub.Repository.Analyzer.Processor
Service repososible for processing content of repository.

## Deployment
Deployment in the current version is manual. Docker compose file will be added in the near future.  You need to run docker images prior to running services:

1. https://github.com/SteeltoeOSS/Dockerfiles/tree/main/rabbitmq
2. https://github.com/SteeltoeOSS/Dockerfiles/tree/main/redis

Then run services:
1. GitHub.Repository.Analyzer.Api
2. GitHub.Repository.Analyzer.Loader
3. GitHub.Repository.Analyzer.Processor

## Configuration
**For project GitHub.Repository.Analyzer.Processor**
In appsettings.json set gRPC -> Url

**For GitHub.Repository.Analyzer.Api**
In appsettings.json set LicenseProcessorClientConfiguration -> LicenseProcessorAddress same like for GitHub.Repository.Analyzer.Processor gRPC -> Url
