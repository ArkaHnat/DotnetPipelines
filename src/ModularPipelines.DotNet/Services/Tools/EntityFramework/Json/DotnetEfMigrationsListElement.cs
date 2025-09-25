namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;

public class DotnetEfMigrationsListElement
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required string SafeName { get; set; }

    public bool? Applied { get; set; }

}
