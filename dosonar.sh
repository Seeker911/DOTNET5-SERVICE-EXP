# start the scanner
dotnet sonarscanner begin /k:"hans-test" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="af8eb54f00e4b8887a4d0f58a3a1ea8d6db01f5c"
# do your build
dotnet build DotNet5ServiceExp.csproj
#stop the scanner
dotnet sonarscanner end /d:sonar.login="af8eb54f00e4b8887a4d0f58a3a1ea8d6db01f5c"
