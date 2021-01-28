#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Blog.Service.NotificationSerder.Api/Blog.Service.NotificationSerder.Api.csproj", "Blog.Service.NotificationSerder.Api/"]
RUN dotnet restore "Blog.Service.NotificationSerder.Api/Blog.Service.NotificationSerder.Api.csproj"
COPY . .
WORKDIR "/src/Blog.Service.NotificationSerder.Api"
RUN dotnet build "Blog.Service.NotificationSerder.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Service.NotificationSerder.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Service.NotificationSerder.Api.dll"]