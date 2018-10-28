#tool "nuget:?package=ReportGenerator"

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");
var buildSettings = new DotNetCoreBuildSettings()
{
    Configuration = configuration,
    ArgumentCustomization = args => args.Append("--no-restore"),
};
var testProjects = GetFiles("./**/*.Tests.csproj");

Information($"Running target {target} in configuration {configuration}");

Task("Clean")
    .Does(() => DotNetCoreClean("."))
    .Does(() => CleanDirectories("./coverage"));

Task("Restore")
    .Does(() => DotNetCoreRestore());
    
Task("Build")
    .Does(() => DotNetCoreBuild(".", buildSettings));
    
Task("Test")
    .DoesForEach(testProjects, project => {
        Information("Testing project " + project);
        DotNetCoreTest(project.ToString(),
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                NoBuild = true,
                ArgumentCustomization = args => args
                    .Append("--no-restore")
                    .Append("/p:CollectCoverage=true")
                    .Append("/p:CoverletOutputFormat=cobertura")
                    .Append($"/p:CoverletOutput=../../coverage/{project.GetFilenameWithoutExtension()}.cobertura.xml"),
            });
    });
    
Task("CoverageReport")
    .Does(() => {
        var reports = testProjects
            .Select(f => $"../../coverage/{f.GetFilenameWithoutExtension()}.cobertura.xml")
            .ToArray();
            
        var reportsString = string.Join(";", reports);
        var argumentsBuilder = new ProcessArgumentBuilder()
            .Append($"-reports:{reportsString}")
            .Append("-targetdir:../../coverage")
            .Append("-reporttypes:Html");
        var settings = new DotNetCoreToolSettings
        {
            WorkingDirectory = new FilePath(testProjects.First().FullPath).GetDirectory().FullPath
        };
        DotNetCoreTool(testProjects.First().FullPath, "reportgenerator", argumentsBuilder, settings);
    });
    
Task("BuildAndTest")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("CoverageReport");

Task("Default")
    .IsDependentOn("BuildAndTest");
    
RunTarget(target);