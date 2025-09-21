using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;
using ModularPipelines.TestHelpers;
using Shouldly;

namespace ModularPipelines.UnitTests;

public class JsonStringSanitizerTests : TestBase
{
	private readonly string sample1 = $@"[2025-09-21T18:58:12.0334570+02:00]|[WARNING]|[XYZ0317/dotnet:23528/:1]|[/Microsoft.EntityFrameworkCore.Model.Validation.]|The decimal property '""AgreementNo""' is part of a key on entity type '""BatchEntry""'. If the configured precision and scale don't match the column type in the database, this will cause values to be silently truncated if they do not fit in the default precision and scale. Consider using a different property as the key, or make sure that the database column type matches the model configuration and enable decimal rounding warnings using 'SET NUMERIC_ROUNDABORT ON'.|{{EventId={{Id=30003, Name=""Microsoft.EntityFrameworkCore.Model.Validation.DecimalTypeKeyWarning""}}, EnvironmentName=""Development"", EnvironmentUserName="" \\ ""}}|
[2025-09-21T18:58:12.5147237+02:00]|[INFORMATION]|[XYZ0317/dotnet:23528/:1]|[/Microsoft.EntityFrameworkCore.Database.Command.]|Executed DbCommand (""17""ms) [Parameters=[""""], CommandType='Text', CommandTimeout='30']""\r\n""""SELECT 1""|{{EventId={{Id=20101, Name=""Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted""}}, EnvironmentName=""Development"", EnvironmentUserName=""Domain\\username""}}|
[2025-09-21T18:58:12.5346460+02:00]|[INFORMATION]|[XYZ0317/dotnet:23528/:1]|[/Microsoft.EntityFrameworkCore.Database.Command.]|Executed DbCommand (""14""ms) [Parameters=[""""], CommandType='Text', CommandTimeout='30']""\r\n""""SELECT OBJECT_ID(N'[__EFMigrationsHistory]');""|{{EventId={{Id=20101, Name=""Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted""}}, EnvironmentName=""Development"", EnvironmentUserName=""\\username""}}|
[2025-09-21T18:58:12.5493075+02:00]|[INFORMATION]|[XYZ0317/dotnet:23528/:1]|[/Microsoft.EntityFrameworkCore.Database.Command.]|Executed DbCommand (""7""ms) [Parameters=[""""], CommandType='Text', CommandTimeout='30']""\r\n""""SELECT [MigrationId], [ProductVersion]\r\nFROM [__EFMigrationsHistory]\r\nORDER BY [MigrationId];""|{{EventId={{Id=20101, Name=""Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted""}}, EnvironmentName=""Development"", EnvironmentUserName=""\\ ""}}|
[
  {{
    ""id"": ""20250916062813_Initial"",
    ""name"": ""Initial"",
    ""safeName"": ""Initial"",
    ""applied"": false
  }}
]";
	[Test]
	public void Verify()
	{
		var output = JsonStringSanitizer.SanitizeOutput(sample1);
		_ = output.ShouldNotBeNull();
	}
}