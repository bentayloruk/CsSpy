#I @"src\packages\FAKE.1.64.8\tools"
#r "FakeLib.dll"
open Fake
open System.IO
open System

let buildTypes = ["Debug";"Release"]
let buildVsVersions = ["2010";"2012"]
let buildOutputPath = @".\build\output"
let releaseOutputPath = @".\build\dist"
let projFiles = !! @"src\**\*CsSpy.fsproj"//Will just get the non-test assemblies.
let version = "0.2"
//We assume build configuration name is %buildType%%vsVersion% (e.g. Debug2012)
let buildConfigNames = 
    [
        for buildType in buildTypes do
            for buildVsVersion in buildVsVersions do
                yield sprintf "%s%s" buildType buildVsVersion
    ]

//Single run tasks.
Target "Clean" (fun _ ->
    CleanDir buildOutputPath 
    CleanDir releaseOutputPath 
)

Run "Clean"

//Once per build configuration tasks.
for buildConfigName in buildConfigNames do
    let name taskName = taskName + buildConfigName
    //Build
    let buildTaskName = name "Build"
    Target buildTaskName (fun _ ->
        MSBuild buildOutputPath "Build" ["Configuration", buildConfigName]  projFiles
        |> Log "BuildOutput:"
    )
    let versionTaskName = name "SetVersion"
    Target versionTaskName (fun _ ->
        AssemblyInfo 
            (fun p -> 
            {p with
                CodeLanguage = FSharp;
                AssemblyVersion = version;
                AssemblyTitle = "CsSpy by Enticify - www.enticify.com";
                AssemblyCopyright = "Copyright Shape Factory Limited " + DateTime.Now.Year.ToString();
                AssemblyDescription = "Visual Studio Debug Visualizers for Commerce Server (" + buildConfigName + ").";
                Guid = "F3E64731-89DF-4D40-8631-C32FD278A476";
                OutputFileName = @".\src\Enticify.CsSpy\AssemblyInfo.fs"})
    )
    //Zip
    let zipTaskName = name "Zip"
    Target zipTaskName (fun _ ->
        let zipFileName = Path.Combine(releaseOutputPath, "CsSpy-" + buildConfigName + "-" + version +  ".zip")
        !+ (buildOutputPath + "\*.*") 
            -- "*.zip" //TODO exclude XML docs too?
            |> Scan
            |> Zip buildOutputPath zipFileName 
    )

    //Do it!
    versionTaskName 
        ==> buildTaskName
        ==> zipTaskName
        |> ignore
    Run zipTaskName 
