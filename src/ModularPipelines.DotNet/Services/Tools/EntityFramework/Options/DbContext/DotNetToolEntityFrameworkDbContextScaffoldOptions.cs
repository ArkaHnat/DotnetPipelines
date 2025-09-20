using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Generates code for a DbContext and entity types for a database. In order for this command to generate an entity type, the database table must have a primary key.
/// </summary>

[CommandPrecedingArguments("scaffold")]
public record DotNetToolEntityFrameworkDbContextScaffoldOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
    public DotNetToolEntityFrameworkDbContextScaffoldOptions() : base()
    {
        CommandParts = ["<CONNECTION>", "<PROVIDER>"];
    }

    /// <summary>
    /// Gets or sets the connection string to the database. For ASP.NET Core 2.x projects, the value can be name=.<name of connection string>. In that case the name comes from the configuration sources that are set up for the project.
    /// </summary>
    [PositionalArgument(PlaceholderName = "<CONNECTION>")]
    public string? Connection { get; set; }

    /// <summary>
    ///     Gets or sets the provider to use. Typically this is the name of the NuGet package, for example: Microsoft.EntityFrameworkCore.SqlServer.
    /// </summary>
    [PositionalArgument(PlaceholderName = "<PROVIDER>")]
    public string? Provider { get; set; }

    /// <summary>
    ///  Gets or sets use attributes to configure the model (where possible). If this option is omitted, only the fluent API is used.
    /// </summary>
    [BooleanCommandSwitch("--data-annotations")]
    public virtual string? DataAnnotations { get; set; }

    /// <summary>
    ///  Gets or sets use attributes to configure the model (where possible). If this option is omitted, only the fluent API is used.
    /// </summary>
    [CommandSwitch("--context")]
    public virtual string? Context { get; set; }

    /// <summary>
    ///  Gets or sets the directory to put the DbContext class file in. Paths are relative to the project directory. Namespaces are derived from the folder names.
    /// </summary>
    [CommandSwitch("--context-dir")]
    public virtual string? ContextDir { get; set; }

    /// <summary>
    ///  Gets or sets the namespace to use for the generated DbContext class. Note: overrides --namespace.
    /// </summary>
    [CommandSwitch("--context-namespace ")]
    public virtual string? ContextNamespace { get; set; }

    /// <summary>
    ///  Gets or sets overwrite existing files.
    /// </summary>
    [BooleanCommandSwitch("--force")]
    public virtual string? Force { get; set; }

    /// <summary>
    ///  Gets or sets the directory to put entity class files in. Paths are relative to the project directory.
    /// </summary>
    [CommandSwitch("--output-dir")]
    public virtual string? OutputDir { get; set; }

    /// <summary>
    ///  Gets or sets the schemas of tables and views to generate entity types for. To specify multiple schemas, repeat --schema for each one. If this option is omitted, all schemas are included. If this option is used, then all tables and views in the schemas will be included in the model, even if they are not explicitly included using --table.
    /// </summary>
    [CommandSwitch("--schema")]
    public virtual string? Schema { get; set; }

    /// <summary>
    ///  Gets or sets the tables and views to generate entity types for. To specify multiple tables, repeat -t or --table for each one. Tables or views in a specific schema can be included using the 'schema.table' or 'schema.view' format. If this option is omitted, all tables and views are included.
    /// </summary>
    [CommandSwitch("--table ")]
    public virtual string? Table { get; set; }

    /// <summary>
    ///  Gets or sets use table, view, sequence, and column names exactly as they appear in the database. If this option is omitted, database names are changed to more closely conform to C# name style conventions.
    /// </summary>
    [CommandSwitch("--use-database-names")]
    public virtual string? UseDatabaseName { get; set; }

    /// <summary>
    ///  Gets or sets suppresses generation of the OnConfiguring method in the generated DbContext class.
    /// </summary>
    [BooleanCommandSwitch("--no-onconfiguring")]
    public virtual string? NoOnConfiguring { get; set; }

    /// <summary>
    ///  Gets or sets don't use the pluralizer.
    /// </summary>
    [BooleanCommandSwitch("--no-pluralize")]
    public virtual string? NoPluralize { get; set; }

    /// <summary>
    ///  Gets or sets the namespace to use for all generated classes. Defaults to generated from the root namespace and the output directory.
    /// </summary>
    [CommandSwitch("--namespace")]
    public virtual string? Namespace { get; set; }
}