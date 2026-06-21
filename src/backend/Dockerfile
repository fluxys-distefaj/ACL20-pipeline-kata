FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /build
COPY . .
RUN dotnet publish src/backend -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
# Linux containers are case-sensitive — match the exact assembly name (here: Backend.dll)
ENTRYPOINT ["dotnet", "Wrong.dll"]