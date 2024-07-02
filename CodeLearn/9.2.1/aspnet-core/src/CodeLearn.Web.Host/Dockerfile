FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/CodeLearn.Web.Host/CodeLearn.Web.Host.csproj", "src/CodeLearn.Web.Host/"]
COPY ["src/CodeLearn.Web.Core/CodeLearn.Web.Core.csproj", "src/CodeLearn.Web.Core/"]
COPY ["src/CodeLearn.Application/CodeLearn.Application.csproj", "src/CodeLearn.Application/"]
COPY ["src/CodeLearn.Core/CodeLearn.Core.csproj", "src/CodeLearn.Core/"]
COPY ["src/CodeLearn.EntityFrameworkCore/CodeLearn.EntityFrameworkCore.csproj", "src/CodeLearn.EntityFrameworkCore/"]
WORKDIR "/src/src/CodeLearn.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/CodeLearn.Web.Host", "src/CodeLearn.Web.Host"]
COPY ["src/CodeLearn.Web.Core", "src/CodeLearn.Web.Core"]
COPY ["src/CodeLearn.Application", "src/CodeLearn.Application"]
COPY ["src/CodeLearn.Core", "src/CodeLearn.Core"]
COPY ["src/CodeLearn.EntityFrameworkCore", "src/CodeLearn.EntityFrameworkCore"]
WORKDIR "/src/src/CodeLearn.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "CodeLearn.Web.Host.dll"]
