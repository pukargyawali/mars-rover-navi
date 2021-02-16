FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./

WORKDIR /src
#Copy dependancies
COPY ["RoverWebApp/RoverWebApp.csproj", "RoverWebApp/"]
COPY ["RoverFileProcessor/RoverFileProcessor.csproj", "RoverFileProcessor/"]
COPY ["RoverControlService/RoverControlService.csproj", "RoverControlService/"]
COPY ["RoverAppTests/RoverAppTests.csproj", "RoverAppTests/"]

WORKDIR /src/RoverWebApp
RUN dotnet restore "RoverWebApp.csproj" --force

COPY . .
RUN dotnet build  - c Release -o /app/build

# Copy everything else and build
COPY .  ./
RUN dotnet publish "RoverWebApp.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/RoverWebApp/out .
ENTRYPOINT ["dotnet", "RoverWebApp.dll"]