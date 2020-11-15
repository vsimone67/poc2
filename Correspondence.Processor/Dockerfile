FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Correspondence.Processor.csproj", ""]
RUN dotnet restore "./Correspondence.Processor.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Correspondence.Processor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Correspondence.Processor.csproj" -c Kubernetes -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Correspondence.Processor.dll"]