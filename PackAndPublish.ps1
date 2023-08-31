param(
    [Parameter(Mandatory)]$version,
    [Parameter(Mandatory)]$key
)

try {
    Set-Location $PSScriptRoot

    Write-Host 'Building' -ForegroundColor White -BackgroundColor DarkGreen
    dotnet build -c Release Simple.CommandsAndQueries\Simple.CommandsAndQueries.csproj

    Write-Host 'Testing .Net 6' -ForegroundColor White -BackgroundColor DarkGreen
    dotnet test Simple.CommandsAndQueries.Tests\Simple.CommandsAndQueries.Tests.csproj -c Release --framework net6.0 -l "console;verbosity=normal" -l "trx;logfilename=Net6TestResults.trx"

    Write-Host 'Testing .Net 7' -ForegroundColor White -BackgroundColor DarkGreen
    dotnet test Simple.CommandsAndQueries.Tests\Simple.CommandsAndQueries.Tests.csproj -c Release --framework net7.0 -l "console;verbosity=normal" -l "trx;logfilename=Net7TestResults.trx"

    Write-Host "Building package v$version" -ForegroundColor White -BackgroundColor DarkGreen
    $year = Get-Date -Format "yyyy"
    dotnet pack /p:PackageVersion=$version /p:AssemblyVersion=$version /p:FileVersion=$version /p:Authors="Barry Dunne" /p:Copyright="Copyright © Barry Dunne $year" /p:Description="This is a basic library if interfaces to use for CQRS commands and queries." -c Release Simple.CommandsAndQueries\Simple.CommandsAndQueries.csproj

    Write-Host "Pushing nupkg and snupkg packages v$version to NuGet" -ForegroundColor White -BackgroundColor DarkGreen
    dotnet nuget push "$PSScriptRoot\Simple.CommandsAndQueries\bin\Release\Simple.CommandsAndQueries.$version.nupkg" -s https://api.nuget.org/v3/index.json -k $key

    Write-Host "Done" -ForegroundColor White -BackgroundColor DarkGreen
}
catch {
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}
