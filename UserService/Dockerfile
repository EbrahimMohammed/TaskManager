FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . .   

# Publish the application
RUN dotnet publish UserService.sln -c Release -o out

# Build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the output from the build stage to the runtime image
COPY --from=build-env /app/out . 

# Set the entry point for the container
ENTRYPOINT [ "dotnet", "UsersService.dll" ]
