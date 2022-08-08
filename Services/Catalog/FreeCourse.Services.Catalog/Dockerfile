#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/FreeCourse.Services.Catalog/FreeCourse.Services.Catalog.csproj", "Services/Catalog/FreeCourse.Services.Catalog/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "Services/Catalog/FreeCourse.Services.Catalog/FreeCourse.Services.Catalog.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/FreeCourse.Services.Catalog"
RUN dotnet build "FreeCourse.Services.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Catalog.dll"]