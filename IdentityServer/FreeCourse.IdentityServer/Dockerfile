#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["IdentityServer/FreeCourse.IdentityServer/FreeCourse.IdentityServer.csproj", "IdentityServer/FreeCourse.IdentityServer/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "IdentityServer/FreeCourse.IdentityServer/FreeCourse.IdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/FreeCourse.IdentityServer"
RUN dotnet build "FreeCourse.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.IdentityServer.dll"]