namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<DotnetEfDbContextListElement>>(myJsonResponse);
public class DotnetEfDbContextListElement
{
    public required string FullName { get; set; }

    public required string SafeName { get; set; }

    public required string Name { get; set; }

    public required string AssemblyQualifiedName { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class DotnetEfDbContextInfoElement
{
    public required string Type { get; set; }

    public required string ProviderName { get; set; }

    public required string DatabaseName { get; set; }

    public required string DataSource { get; set; }

    public required string Options { get; set; }
}
