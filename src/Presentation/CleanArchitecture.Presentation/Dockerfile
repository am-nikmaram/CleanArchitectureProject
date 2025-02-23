# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj", "src/Presentation/CleanArchitecture.Presentation/"]
COPY ["src/Presentation/CleanArchitecture.WebFramework/CleanArchitecture.WebFramework.csproj", "src/Presentation/CleanArchitecture.WebFramework/"]
COPY ["src/Core/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/Core/CleanArchitecture.Application/"]
COPY ["src/Core/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/Core/CleanArchitecture.Domain/"]
COPY ["src/Shared/CleanArchitecture.Shared/CleanArchitecture.Shared.csproj", "src/Shared/CleanArchitecture.Shared/"]
COPY ["src/Infrastructure/CleanArchitecture.Infrastructure.Identity/CleanArchitecture.Infrastructure.Identity.csproj", "src/Infrastructure/CleanArchitecture.Infrastructure.Identity/"]
COPY ["src/Infrastructure/CleanArchitecture.Infrastructure.Persistence/CleanArchitecture.Infrastructure.Persistence.csproj", "src/Infrastructure/CleanArchitecture.Infrastructure.Persistence/"]
RUN dotnet restore "./src/Presentation/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Presentation/CleanArchitecture.Presentation"
RUN dotnet build "./CleanArchitecture.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchitecture.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Presentation.dll"]