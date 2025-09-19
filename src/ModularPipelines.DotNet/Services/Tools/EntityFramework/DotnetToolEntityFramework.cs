using System.Diagnostics.CodeAnalysis;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Models;

namespace ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

[ExcludeFromCodeCoverage]
public class DotnetToolEntityFramework
{
    public DotnetToolEntityFramework(ICommand internalCommand)
    {
        _command = internalCommand;
        DbContext = new DotnetToolEntityFrameworkDbContext(_command);
    }

    private readonly ICommand _command;

    public virtual async Task<CommandResult> Install(DotnetToolEntityFrameworkOptions? options = default, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotnetToolEntityFrameworkOptions(), token);
    }

    public DotnetToolEntityFrameworkDbContext DbContext { get; }

	public DotnetToolEntityFrameworkMigrations Migrations { get; }
    
}
