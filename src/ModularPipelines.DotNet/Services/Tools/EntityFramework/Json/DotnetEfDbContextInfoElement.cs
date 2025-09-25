namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;

public class DotnetEfDbContextInfoElement
{
	public required string Type { get; set; }

	public required string ProviderName { get; set; }

	public required string DatabaseName { get; set; }

	public required string DataSource { get; set; }

	public required string Options { get; set; }
}
