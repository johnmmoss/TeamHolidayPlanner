var target = Argument("Target", "Build");
var configuration = Argument("Configuration", "Release");

Task("Restore")
	.Does(() =>
{
		NuGetRestore("TeamHolidayPlanner.sln");
});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() =>
{
	Information("Building TeamHolidayPlaner.sln");

	DotNetBuild(
			  "TeamHolidayPlanner.sln",
			  settings => settings
							.SetConfiguration(configuration)
							.WithTarget("Build"));
});

RunTarget(target);
