FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
# Copy only the file necessary to do the restore so that as long as we don't add any references we can avoid doing a
# restore each time we rebuild
COPY ./taskr.sln ./
COPY ./Server/taskr.Server.csproj ./Server/
COPY ./Client/taskr.Client.csproj ./Client/
COPY ./Shared/taskr.Shared.csproj ./Shared/
RUN dotnet restore
# Then copy everything else for the regular build
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "taskr.Server.dll"]