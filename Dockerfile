

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./

RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_Enviornment=Development \
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps perfomance
    NUGET_XMLDOC_MODE=skip

ENTRYPOINT ["dotnet", "ReactMaterial.dll"]


FROM node:8.2
COPY front/ ./
RUN npm insall
RUN npm start