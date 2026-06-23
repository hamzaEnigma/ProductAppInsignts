# Insurance Quote API

## Overview

REST API for automobile insurance quotation.

## Tech Stack

- .NET 8
- Azure Application Insights
- Azure fonctions

## Architecture

Description of services and dependencies.

## Getting Started

### Prerequisites

- .NET 8 SDK
 
## Setup local
1. Copier appsettings.Development.json.example
2. Renommer en appsettings.Development.json
3. Remplacer YOUR_CONNECTION_STRING_HERE par ta vraie valeur
### Run locally

```bash
dotnet restore
dotnet build
dotnet run
```

## Configuration

Configuration is managed through:

- appsettings.json
- environment variables

## Observability

- Application Insights
- Structured logging
- Health checks

## Deployment using KUDO inside azure portal
	## Build + Publish : 
	## delete the file if exists before this command
	 Remove-Item -Path "./publish" -Recurse -Force 
	 dotnet publish ProductSimple.csproj --configuration Release --output ./publish
	## zip published code
	Remove-Item -Path "./app.zip" -Force 
	Compress-Archive -Path ./publish/* -DestinationPath ./app.zip -Force

