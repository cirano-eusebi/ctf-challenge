#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update && apt-get install -y make gcc
WORKDIR /src
COPY ["Server/CTFChallenge.Server.csproj", "Server/"]
COPY ["Client/CTFChallenge.Client.csproj", "Client/"]
COPY ["Shared/CTFChallenge.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/CTFChallenge.Server.csproj"
COPY . .
RUN mkdir -p /app/build
WORKDIR "/src/Security"
RUN make dll buildOpts="-fPIC" dllOpts="-fPIC" buildOutput="/app/build/CTFChallenge.Security.o" dllOutput="/app/build/CTFChallenge.Security.dll"
WORKDIR "/src/Server"
RUN dotnet build "CTFChallenge.Server.csproj" -c Debug -o /app/build


FROM build AS publish
RUN dotnet publish "CTFChallenge.Server.csproj" -c Debug -o /app/publish /p:UseAppHost=false
RUN cp -p /app/build/CTFChallenge.Security.dll /app/publish

FROM base AS final
WORKDIR /app
COPY Flag2of3.txt /flag.txt
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CTFChallenge.Server.dll"]