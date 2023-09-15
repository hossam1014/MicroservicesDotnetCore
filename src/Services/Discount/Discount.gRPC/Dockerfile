FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/Discount/Discount.gRPC/Discount.gRPC.csproj", "Services/Discount/Discount.gRPC/"]
RUN dotnet restore "Services/Discount/Discount.gRPC/Discount.gRPC.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.gRPC"
RUN dotnet build "Discount.gRPC.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Discount.gRPC.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.gRPC.dll"]
