#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/Super.Ticket.WebApi/Super.Ticket.WebApi.csproj", "Src/Super.Ticket.WebApi/"]
COPY ["Src/Super.Ticket.Application/Super.Ticket.Application.csproj", "Src/Super.Ticket.Application/"]
COPY ["Src/Super.Ticket.Persistence/Super.Ticket.Persistence.csproj", "Src/Super.Ticket.Persistence/"]
COPY ["Src/Super.Ticket.Domain/Super.Ticket.Domain.csproj", "Src/Super.Ticket.Domain/"]
COPY ["Src/Super.Ticket.Infrastructure/Super.Ticket.Infrastructure.csproj", "Src/Super.Ticket.Infrastructure/"]
RUN dotnet restore "Src/Super.Ticket.WebApi/Super.Ticket.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/Super.Ticket.WebApi"
RUN dotnet build "Super.Ticket.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Super.Ticket.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Super.Ticket.WebApi.dll"]