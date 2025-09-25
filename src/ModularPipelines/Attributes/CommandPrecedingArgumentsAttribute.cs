namespace ModularPipelines.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CommandPrecedingArgumentsAttribute : Attribute
{
    public CommandPrecedingArgumentsAttribute(params string[] precedingArguments)
    {
        PrecedingArguments = precedingArguments;
    }

    public string[] PrecedingArguments { get; }
}
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CommandFollowingArgumentsAttribute : Attribute
{
	public CommandFollowingArgumentsAttribute(params string[] followingArguments)
	{
		FollowingArguments = followingArguments;
	}

	public string[] FollowingArguments { get; }
}