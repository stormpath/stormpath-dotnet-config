var configuration = Argument("configuration", "Debug");

Task("Clean")
.Does(() =>
{
    CleanDirectory("./artifacts/");
});

Task("Restore")
.Does(() => 
{
    DotNetCoreRestore();
});

Task("Build")
.Does(() =>
{
    DotNetCoreBuild("./src/**/project.json", new DotNetCoreBuildSettings
    {
        Configuration = configuration
    });
});

Task("Pack")
.Does(() =>
{
    var projects = GetFiles("./src/**/project.json");
    Console.WriteLine("Packing {0} projects", projects.Count());

    foreach (var project in projects)
    {
        DotNetCorePack(project.FullPath, new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts/"
        });
    }
});

Task("Test")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.Does(() =>
{
    var tests = GetFiles("./test/**/project.json");
    Console.WriteLine("Running {0} tests", tests.Count());

    foreach (var test in tests) 
    {
        DotNetCoreTest(test.FullPath);
    }
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Pack");


var target = Argument("target", "Default");
RunTarget(target);