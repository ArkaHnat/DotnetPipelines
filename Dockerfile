FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build

RUN git --version
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore ModularPipelines.Merged.sln
# Build and publish a release
#RUN dotnet build ModularPipelines.Merged.sln
WORKDIR "/App/src/ModularPipelines.Build"
RUN dotnet run -c Release --framework net9.0 #--project ModularPipelines.Build.csproj
