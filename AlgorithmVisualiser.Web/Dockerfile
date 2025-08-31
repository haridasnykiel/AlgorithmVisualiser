FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AlgorithmVisualiser.Web/AlgorithmVisualiser.Web.csproj", "AlgorithmVisualiser.Web/"]
RUN dotnet restore "AlgorithmVisualiser.Web/AlgorithmVisualiser.Web.csproj"
COPY . .
WORKDIR "/src/AlgorithmVisualiser.Web"
RUN dotnet publish "./AlgorithmVisualiser.Web.csproj" -c $BUILD_CONFIGURATION

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=publish /src/AlgorithmVisualiser.Web/bin/Release/net8.0/publish/wwwroot .
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]