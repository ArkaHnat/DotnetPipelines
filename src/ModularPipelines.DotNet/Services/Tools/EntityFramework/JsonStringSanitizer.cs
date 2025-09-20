using System.Text.RegularExpressions;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;

public static class JsonStringSanitizer
{
	public static string SanitizeOutput(string standardOutput)
	{
		var matches = Regex.Matches(standardOutput, @"(\[\s*[{\s\S]*?}\s*\]", RegexOptions.Multiline);
		if (matches.Count > 0)
		{
			return matches[matches.Count - 1].Value;
		}
		return "[]";
	}
}