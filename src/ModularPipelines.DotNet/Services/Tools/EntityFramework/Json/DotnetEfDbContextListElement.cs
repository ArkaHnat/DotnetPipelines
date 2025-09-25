namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;

public class DotnetEfDbContextListElement
{
	public required string FullName { get; set; }

	public required string SafeName { get; set; }

	public required string Name { get; set; }

	public required string AssemblyQualifiedName { get; set; }
}
