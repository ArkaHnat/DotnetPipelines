namespace ModularPipelines.Engine;

internal interface IDependencyDetector
{
    void Check();

    void ResolveRelations();
}