using ModularPipelines.Git.Models;

namespace ModularPipelines.Git;

public interface IGitVersioning
{
    Task<GitVersionInformation> GetGitVersioningInformation();

    public string DotnetToolName { get; }
}