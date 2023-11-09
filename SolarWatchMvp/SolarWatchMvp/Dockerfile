FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy user secrets to the container
COPY appsettings.json ./

# Set environment variables with placeholders
ENV IssueAudience=${ISSUE_AUDIANCE}
ENV ConnectionString=${CONNECTION_STRING}
ENV ServiceApiKey=${SERVICE_API_KEY}
ENV IssueSign=${SOMETHING_SECRET}

# Copy published output
COPY --from=build-env /app/out .

# Set up ASP.NET Core URL
ENV ASPNETCORE_URLS=http://+:80

# Run the application
ENTRYPOINT ["dotnet", "SolarWatchMvp.dll"]