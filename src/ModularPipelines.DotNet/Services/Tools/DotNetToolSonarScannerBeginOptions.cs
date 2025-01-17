using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools;

public record DotNetToolSonarScannerBeginOptions : DotNetOptions, IDotnetToolSonarScanner
{
	public DotNetToolSonarScannerBeginOptions(string projectKey, string organization)
	{
		CommandParts = ["tool", "run", IDotnetToolSonarScanner.PackageNameConst, "begin"];
		ProjectKey = projectKey;
		this.Organization = organization;
	}

	/// <summary>
	/// Gets or sets /k:.<project-key>
	/// [required] Specifies the key of the analyzed project in SonarQube Server.
	/// </summary>
	[CommandSwitch("/key", ":")]
	public string ProjectKey { get; set; }

	/// <summary>
	/// Gets or sets /o:"<organization>"
	/// </summary>
	[CommandSwitch("/o", ":")]
	public string Organization { get; set; }
	/// <summary>
	/// Gets or sets /n:.<project name>
	/// [optional] Specifies the name of the analyzed project in SonarQube Server.
	/// Adding this argument will overwrite the project name in SonarQube Server if it already exists.
	/// </summary>
	[CommandSwitch("/n",":")]
	public string? ProjectName { get; set; }

	/// <summary>
	/// Gets or sets /v:.<project name>
	/// [recommended] Specifies the version of your project.
	/// </summary>
	[CommandSwitch("/v", ":")]
	public string? Version { get; set; }

	/// <summary>
	/// Gets or sets /d:sonar.token=.<token> 
	/// [recommended] Requires version 5.13+. Use sonar.login for earlier versions.
	/// Specifies the authentication token used to authenticate with SonarQube Server. 
	/// If this argument is added to the Begin step, it must also be added to the End step.
	/// </summary>
	[CommandSwitch("/d:sonar.token","=")]
	public string? SonarToken { get; set; }

	/// <summary>
	/// Gets or sets /d:sonar.clientcert.path=.<ClientCertificatePath> 
	/// [optional] Specifies the path to a client certificate used to access SonarQube Server if mutual TLS is used. The certificate must be password-protected.
	/// </summary>
	[CommandSwitch("/d:sonar.clientcert.path", "=")]
	public string? ClientCertificatePath { get; set; }

	/// <summary>
	/// Gets or sets /d:sonar.clientcert.password=.<ClientCertificatePassword> 
	/// [optional] Specifies the password for the client certificate used to access SonarQube Server if mutual TLS is used. If this argument is added to the Begin step, it must also be added to the End step.
	/// </summary>
	[CommandSwitch("/d:sonar.clientcert.password", "=")]
	public string? ClientCertificatePassword { get; set; }

	/// <summary>
	/// Gets or sets /d:sonar.verbose=.<boolean>"
	/// [optional] Sets the logging verbosity to detailed. Add this argument before sending logs for troubleshooting.
	/// </summary>
	[CommandSwitch("/d:sonar.verbose","=")]
	public bool? Verbose { get; set; }

	/// <summary>
	/// Gets or sets /d:sonar.dotnet.excludeTestProjects=true 
	/// [optional] Excludes Test Projects from analysis. Add this argument to improve build performance when issues should not be detected in Test Projects.
	/// </summary>
	[CommandSwitch("/d:sonar.dotnet.excludeTestProjects", "=")]
	public bool? ExcludeTestProjects { get; set; }

	/// <summary>Gets or sets /d:sonar.http.timeout=60 
	/// [optional] Specifies the time in seconds to wait before the HTTP requests time out.
	/// </summary>
	[CommandSwitch("/d:sonar.http.timeout", "=")]
	public int? Timeout { get; set; }

	/// <summary>
	/// Gets or sets /d:.<analysis-parameter>=<value> 
	/// [optional] Specifies an additional SonarQube Server analysis parameter, you can add this argument multiple times. Please note that the sonar.sources and sonar.tests parameters are not supported.
	/// https://docs.sonarsource.com/sonarqube-server/latest/analyzing-source-code/analysis-parameters/
	/// </summary>
	[CommandSwitch("/d:<analysis-parameter>", "=")]
	public List<string>? AnalysisParameters { get; set; }

	/// <summary>
	/// Gets or sets /s:.<custom.analysis.xml> 
	/// [optional] Overrides the $install_directory/SonarQube.Analysis.xml.You need to give the absolute path to the file.
	/// </summary>
	[CommandSwitch("/s", ":")]
	public string? CustomAnalysisXml { get; set; }

	/// <summary>
	/// Gets or sets /d:sonar.plugin.cache.directory=.<path_to_directory> 
	/// [optional] Requires version 5.15+. Overrides the path where the scanner downloads its plugins.Plugins that are already present will not be downloaded again unless newer versions are available.
	/// You can provide a relative or an absolute path.
	/// Defaults to the machine's temporary files directory.
	/// </summary>
	[CommandSwitch("/d:sonar.plugin.cache.directory", "=")]
	public string? SonarPluginCacheDirectory { get; set; }

	/// <summary>Gets or sets /d:sonar.scanner.scanAll=.<boolean> 
	/// [optional] Enables and Disables the analysis of multiple file types.See the Multi-language support article for the full details.Unless manually excluded, the files linked by the.csproj project file will be analyzed even if the value is false.
	/// Default: true

	// </summary>
	[CommandSwitch("/d:sonar.scanner.scanAll", "=")]
	public bool? ScanAll { get; set; }
}