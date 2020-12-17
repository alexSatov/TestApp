$currentLocation = Get-Location
Set-Location ./ApiService1/
docker build -t apiservice1 .
Set-Location ../ApiService2/
docker build -t apiservice2 .
docker-compose up -d
Set-Location $currentLocation
