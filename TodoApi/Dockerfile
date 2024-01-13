# Use Microsoft's official build .NET image.
# https://hub.docker.com/_/microsoft-dotnet-core-sdk/
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the project
COPY . ./
RUN dotnet publish -c Release -o out

# Test stage
FROM build AS test
WORKDIR /app
COPY . ./
RUN dotnet test --no-restore --verbosity normal

# Use Microsoft's official runtime .NET image.
# https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

# Make sure the app binds to port 80
ENV ASPNETCORE_URLS http://*:5299

# Start the app
ENTRYPOINT ["dotnet", "TodoApi.dll"]