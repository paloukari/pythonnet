FROM microsoft/dotnet:2.1-runtime AS base
RUN apt-get update -y && apt-get install python3 -y && apt-get install python3-pip -y
RUN ldconfig

WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY DockerTest/DockerTest.csproj DockerTest/
RUN dotnet restore DockerTest/DockerTest.csproj
COPY . .
WORKDIR /src/DockerTest
RUN dotnet build DockerTest.csproj -c ReleasePy3 -o /app

FROM build AS publish
RUN dotnet publish DockerTest.csproj -c ReleasePy3 -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN pip3 install --no-cache-dir -r requirements.txt

ENV LD_LIBRARY_PATH /usr/lib/python3.5/config-3.5m-x86_64-linux-gnu

ENTRYPOINT ["dotnet", "DockerTest.dll"]
