param(
    [Parameter(Mandatory)]$version,
    [Parameter(Mandatory)]$key
)

try {
    Set-Location $PSScriptRoot

    $year = Get-Date -Format "yyyy"

    Write-Host "Building package v$version" -ForegroundColor White -BackgroundColor DarkGreen
    dotnet pack /p:PackageVersion=$version /p:AssemblyVersion=$version /p:FileVersion=$version /p:Authors="Barry Dunne" /p:Copyright="Copyright © Barry Dunne $year" /p:Description="This is a basic library if interfaces to use for CQRS commands and queries." -c Release Simple.CommandsAndQueries\Simple.CommandsAndQueries.csproj

    Write-Host "Pushing nupkg and snupkg packages v$version to NuGet" -ForegroundColor White -BackgroundColor DarkGreen
    dotnet nuget push "$PSScriptRoot\Simple.CommandsAndQueries\bin\Release\Simple.CommandsAndQueries.$version.nupkg" -s https://api.nuget.org/v3/index.json -k $key

    Write-Host "Done" -ForegroundColor White -BackgroundColor DarkGreen
}
catch {
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}
