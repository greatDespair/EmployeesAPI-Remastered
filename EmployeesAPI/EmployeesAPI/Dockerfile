#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmployeesAPI/EmployeesAPI.csproj", "EmployeesAPI/"]
COPY ["EmployeesAPI.Domain/EmployeesAPI.Domain.csproj", "EmployeesAPI.Domain/"]
COPY ["EmployeesAPI.Data/EmployeesAPI.Data.csproj", "EmployeesAPI.Data/"]
COPY ["EmployeesAPI.Models/EmployeesAPI.Models.csproj", "EmployeesAPI.Models/"]
RUN dotnet restore "EmployeesAPI/EmployeesAPI.csproj"
COPY . .
WORKDIR "/src/EmployeesAPI"
RUN dotnet build "EmployeesAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeesAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeesAPI.dll"]
