# Insurance Quote API

## Overview

REST API for automobile insurance quotation.

## Tech Stack

- .NET 8
- Azure Application Insights


## Architecture

Description of services and dependencies.

## Getting Started

### Prerequisites

- .NET 9 SDK
- Docker
- SQL Server

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
	## Build + Publish : delete the file if exists before this command
	dotnet publish ProductSimple.csproj --configuration Release --output ./publish
	## zip published code
	Compress-Archive -Path ./publish/* -DestinationPath ./app.zip -Force

