#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"
#tool coveralls.net
#tool coveralls.io

#addin nuget:?package=Cake.AppVeyor
#addin "Cake.FileHelpers"
#addin Cake.Coveralls
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./bin") + Directory(configuration);
var artifactsDir = Directory("./artifacts");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    CleanDirectory(artifactsDir);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore("./", new DotNetCoreRestoreSettings
    {
        Verbose = false,
        Verbosity = DotNetCoreRestoreVerbosity.Warning
    });
        
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var projects = GetFiles("./**/*.xproj");
    foreach(var project in projects)
    {
        DotNetCoreBuild(project.GetDirectory().FullPath, new DotNetCoreBuildSettings
        {
            Verbose = false            
        });
    }
});
 
Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./test/**/*.xproj");
    foreach(var project in projects)
    {
       var name = testProjectName(project.GetDirectory().FullPath);
       OpenCover(tool => {
        tool.DotNetCoreTest(project.GetDirectory().FullPath, new DotNetCoreTestSettings {
                NoBuild = true,
                Verbose = false
            });   
        },
        new FilePath( artifactsDir.ToString() + "/result.xml"),
        new OpenCoverSettings(){
                OldStyle = true,
                Filters = {"+[" + name + "]* -[Test*]*"},
                ArgumentCustomization = args=>args.Append("-mergeoutput")
        });
    }       
});

Task("Publish-Test-Results")
    .IsDependentOn("Run-Tests")
    .Does(() =>
{
    if(AppVeyor.IsRunningOnAppVeyor)
    {
        if (HasEnvironmentVariable("COVERALLS_REPO_TOKEN"))
        {
            CoverallsIo(artifactsDir.ToString() + "/result.xml",new CoverallsIoSettings()
            {
                RepoToken = EnvironmentVariable("COVERALLS_REPO_TOKEN")
            });
        }
    }
    else
    {
        ReportGenerator(artifactsDir.ToString() + "/**/*.xml",
        artifactsDir.ToString() + "/report"); 
    }     
});

Task("Publish")
    .IsDependentOn("Publish-Test-Results")
    .Does(() =>
{
    var projects = GetFiles("./src/api/*.xproj");    
    foreach(var project in projects)
    {
        DotNetCorePublish(project.GetDirectory().FullPath, new DotNetCorePublishSettings
        {
            OutputDirectory = buildDir,
            NoBuild = false   
        });
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);

public static string testProjectName(string filePath)
{
    return filePath.Split('/').Last().Replace(".Test","");
}