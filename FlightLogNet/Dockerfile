FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine
WORKDIR /app
COPY ./bin/Release/net7.0/linux-musl-x64/publish ./
ENTRYPOINT ["./FlightLogNet"]