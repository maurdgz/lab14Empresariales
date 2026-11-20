FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish Lab14_MauricioRdgz/Lab14_MauricioRdgz.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
USER $APP_UID
ENTRYPOINT ["dotnet", "Lab14_MauricioRdgz.dll"]
