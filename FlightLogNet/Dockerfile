FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine
WORKDIR /app
COPY ./bin/Release/net5.0/linux-musl-x64/publish ./
ENTRYPOINT ["./FlightLogNet"]