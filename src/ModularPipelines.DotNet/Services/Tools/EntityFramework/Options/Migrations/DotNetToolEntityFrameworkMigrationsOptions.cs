using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;

public record DotNetToolEntityFrameworkMigrationsOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
    public DotNetToolEntityFrameworkMigrationsOptions() : base()
    {
		CommandParts.Add("migrations");
    }

    /// <summary>
    ///     Gets or sets the file to write the result to.
    /// </summary>
    [CommandSwitch("--output ")]
    public virtual string? Output { get; set; }
}