#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/PhotoStock/FreeCourse.Services.PhotoStock/FreeCourse.Services.PhotoStock.csproj", "Services/PhotoStock/FreeCourse.Services.PhotoStock/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "Services/PhotoStock/FreeCourse.Services.PhotoStock/FreeCourse.Services.PhotoStock.csproj"
COPY . .
WORKDIR "/src/Services/PhotoStock/FreeCourse.Services.PhotoStock"
RUN dotnet build "FreeCourse.Services.PhotoStock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.PhotoStock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.PhotoStock.dll"]