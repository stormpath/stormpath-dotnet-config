$exitCode = 0

dnx -p test\Microsoft.Extensions.Configuration.Contrib.Stormpath.EnvironmentVariables.Test test
$exitCode = $LASTEXITCODE

dnx -p test\Microsoft.Extensions.Configuration.Contrib.Stormpath.ObjectReflection.Test test
$exitCode = $LASTEXITCODE

dnx -p test\Microsoft.Extensions.Configuration.Contrib.Stormpath.PropertiesFile.Test test
$exitCode = $LASTEXITCODE

dnx -p test\Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml.Test test
$exitCode = $LASTEXITCODE

dnx -p test\Stormpath.Configuration.Test test
$exitCode = $LASTEXITCODE

return $exitCode
