FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["EClinic.csproj", "./"]
RUN dotnet restore "EClinic.csproj"
COPY . .
RUN dotnet publish "EClinic.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EClinic.dll"]
