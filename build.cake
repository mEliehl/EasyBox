#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"
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
var artifactsDir = Directory("./artifacts") + Directory(configuration);
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

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./test/**/*.xproj");
    
    foreach(var project in projects)
    {
       var name = testProjectName(project.GetDirectory().FullPath);
       var dir = Directory(artifactsDir.ToString()) + Directory(name);
       CreateDirectory(dir);
       OpenCover(tool => {
        tool.DotNetCoreTest(project.GetDirectory().FullPath, new DotNetCoreTestSettings {
                NoBuild = true,
                Verbose = false
            });   
        },
        new FilePath( dir.ToString() + "/result.xml"),
        new OpenCoverSettings(){
                OldStyle = true,
                Filters = {"+[" + name + "]* -[Test*]*"}
        });
    }    

    ReportGenerator(artifactsDir.ToString() + "/**/*.xml",
     artifactsDir.ToString() + "/report.xml");
});

Task("Publish")
    .IsDependentOn("Run-Unit-Tests")
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