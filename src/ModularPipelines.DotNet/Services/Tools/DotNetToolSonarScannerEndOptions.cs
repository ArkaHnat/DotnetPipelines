using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools;

public record DotNetToolSonarScannerEndOptions : DotNetOptions
{

	public DotNetToolSonarScannerEndOptions() : base()
	{
		CommandParts = ["tool", "run", IDotnetToolSonarScanner.PackageNameConst, "end"];
	}

	/// <summary>
	/// Gets or sets /d:sonar.token=.<token> 
	/// [recommended] Requires version 5.13+. Use sonar.login for earlier versions.
	/// Specifies the authentication token used to authenticate with SonarQube Server. 
	/// If this argument is added to the Begin step, it must also be added to the End step.
	/// </summary>
	[CommandSwitch("/d:sonar.token", "=")]
	public string? SonarToken { get; set; }
	/// <summary>
	/// Gets or sets /d:sonar.clientcert.password=.<ClientCertificatePassword> 
	/// [optional] Specifies the password for the client certificate used to access SonarQube Server if mutual TLS is used. If this argument is added to the Begin step, it must also be added to the End step.
	/// </summary>
	[CommandSwitch("/d:sonar.clientcert.password", "=")]
	public string? ClientCertificatePassword { get; set; }
}