#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/FakePayment/FreeCourse.Services.FakePayment/FreeCourse.Services.FakePayment.csproj", "Services/FakePayment/FreeCourse.Services.FakePayment/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
RUN dotnet restore "Services/FakePayment/FreeCourse.Services.FakePayment/FreeCourse.Services.FakePayment.csproj"
COPY . .
WORKDIR "/src/Services/FakePayment/FreeCourse.Services.FakePayment"
RUN dotnet build "FreeCourse.Services.FakePayment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.FakePayment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.FakePayment.dll"]