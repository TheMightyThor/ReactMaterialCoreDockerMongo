FROM microsoft/dotnet:sdk AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS https://+:5001;http://+:5000
ENV ASPNETCORE_ENVIORNMENT Development
EXPOSE 5001
EXPOSE 5000
ENTRYPOINT ["dotnet", "FinancePoc.dll"]
