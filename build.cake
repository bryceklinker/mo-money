#tool "nuget:?package=ReportGenerator"

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information($"Running target {target} in configuration {configuration}");

Task("Restore")
    .Does(() => {
        DotNetCoreRestore();
    });
    
Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild(".",
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });
    
Task("Test")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.Tests.csproj");
        foreach(var project in projects)
        {
            Information("Testing project " + project);
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    ArgumentCustomization = args => args
                        .Append("--no-restore")
                        .Append("/p:CollectCoverage=true")
                        .Append("/p:CoverletOutputFormat=cobertura")
                        .Append($"/p:CoverletOutput=../../coverage/{project.GetFilenameWithoutExtension()}.cobertura.xml")
                        .Append("--logger trx"),
                });
        }
    });
    
Task("CoverageReport")
    .Does(() => {
        var projects = GetFiles("./**/*.Tests.csproj");
        var reports = projects
            .Select(f => $"./coverage/{f.GetFilenameWithoutExtension()}.cobertura.xml")
            .ToArray();
        ReportGenerator(string.Join(";", reports), "./coverage", new ReportGeneratorSettings
        {
            ReportTypes = new []
            {
                ReportGeneratorReportType.Html
            }
        });
    });
    
Task("BuildAndTest")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("CoverageReport");

Task("Default")
    .IsDependentOn("BuildAndTest");
    
RunTarget(target);