using CliWrap.Buffered;
using ModularPipelines.Context;
using ModularPipelines.Options;

namespace ModularPipelines.Node;

public class Nvm : INvm
{
    private readonly IModuleContext _context;

    public Nvm(IModuleContext context)
    {
        _context = context;
    }
    
    public Task<BufferedCommandResult> Use(string version, CancellationToken cancellationToken = default)
    {
        return _context.Command.UsingCommandLineTool(new CommandLineToolOptions("nvm")
        {
            Arguments = new []{ "use", version }
        }, cancellationToken);
    }
}