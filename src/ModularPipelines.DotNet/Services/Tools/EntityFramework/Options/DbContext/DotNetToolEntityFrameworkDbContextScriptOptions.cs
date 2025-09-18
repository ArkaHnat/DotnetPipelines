using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Generates a SQL script from the DbContext. Bypasses any migrations.
/// </summary>
public record DotNetToolEntityFrameworkDbContextScriptOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
    public DotNetToolEntityFrameworkDbContextScriptOptions() : base()
    {
        CommandParts.Add("Script");
    }

    /// <summary>
    ///     Gets or sets the file to write the result to.
    /// </summary>
    [CommandSwitch("--output ")]
    public virtual string? Output { get; set; }
}