FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY ./Naheulbook.Web/bin/Release/publish/ /app
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://localhost:5000
EXPOSE 5000
ENTRYPOINT "./Naheulbook.Web"