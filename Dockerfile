FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build

RUN git --version
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
WORKDIR "/App/src/ModularPipelines.Build"
RUN dotnet restore ModularPipelines.Build.csproj
# Build and publish a release
RUN dotnet build ModularPipelines.Build.csproj -c Release --no-restore

RUN dotnet run ModularPipelines.Build.csproj -c Release --no-build --framework net9.0 
