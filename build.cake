#load build/path.cake

var target = Argument("Target", "Build");
var configuration = Argument("Configuration", "Release");

Task("Restore")
	.Does(() =>
{
		NuGetRestore(Paths.SolutionFile);
});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() =>
{
	Information("Building TeamHolidayPlaner.sln");

	DotNetBuild(
			  Paths.SolutionFile,
			  settings => settings
							.SetConfiguration(configuration)
							.WithTarget("Build"));
});

RunTarget(target);
