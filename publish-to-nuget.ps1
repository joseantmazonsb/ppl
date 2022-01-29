$apikey = ""
$folders = Get-ChildItem -Path . -Directory -Force -ErrorAction SilentlyContinue -Exclude @(".*", "*.Test") | Select-Object -ExpandProperty FullName
foreach ($folder in $folders) {
    $pkg = Get-ChildItem $folder\bin\release -File -Filter "*.nupkg" | Sort-Object Name | Select-Object -last 1 -ExpandProperty FullName
    Write-Output "Publishing '$pkg'..."
    dotnet nuget push $pkg --api-key $apikey --source https://api.nuget.org/v3/index.json --skip-duplicate
}