# .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS dotnet

# Copy Projects
COPY source/Web/Web.csproj ./source/Web/

# .NET Restore
RUN dotnet restore ./source/Web/Web.csproj

# Copy All Files
COPY source ./source/

# .NET Publish
RUN dotnet publish ./source/Web/Web.csproj -c Release -o /dist --no-restore

# Angular
FROM node:16-alpine AS angular
WORKDIR ./source/Web/Frontend/
COPY source/Web/Frontend/package.json ./
RUN npm run restore
COPY source/Web/Frontend ./
RUN npm run publish

# .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=dotnet /dist .
COPY --from=angular /source/Web/Frontend/dist ./Frontend
ENTRYPOINT ["dotnet", "Web.dll"]
