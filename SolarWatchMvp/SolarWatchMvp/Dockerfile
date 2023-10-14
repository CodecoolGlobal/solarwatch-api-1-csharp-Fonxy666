#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["SolarWatchMvp.csproj", "SolarWatchMvp/"]
#RUN dotnet restore "SolarWatchMvp/SolarWatchMvp.csproj"
#COPY . .
#WORKDIR "/src/SolarWatchMvp"
#RUN dotnet build "SolarWatchMvp.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "SolarWatchMvp.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SolarWatchMvp.dll"]

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Set the environment variable to specify the port
ENV ASPNETCORE_URLS=http://+:80

# Copy the published output from the previous stage
COPY --from=build-env /app/out .

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "SolarWatch.dll"]
