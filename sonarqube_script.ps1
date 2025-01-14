# Define variables for project key, SonarQube URL, and token
$projectKey = "SmartRealEstateManagementSystem"
$sonarUrl = "http://localhost:9000"
$sonarToken = "sqp_f9f7fd46b345d847552fa186709674f3c9c60a4c"
$coverageReportPath = "coverage.xml"

# Begin SonarScanner analysis
dotnet sonarscanner begin `
    /k:"$projectKey" `
    /d:sonar.host.url="$sonarUrl" `
    /d:sonar.token="$sonarToken" `
    /d:sonar.cs.vscoveragexml.reportsPaths="$coverageReportPath"

# Build the project without incremental compilation
dotnet build --no-incremental

# Collect coverage report
dotnet-coverage collect "dotnet test" -f xml -o "$coverageReportPath"

# End SonarScanner analysis
dotnet sonarscanner end /d:sonar.token="$sonarToken"
