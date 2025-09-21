using Newtonsoft.Json.Linq;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;

public static class JsonStringSanitizer
{
    public static string? SanitizeOutput(string output)
    {
        if (string.IsNullOrEmpty(output))
        {
            return null;
        }

        var index = output.Length - 1;
        var bracketCount = 0;
        var inString = false;
        var escapeNext = false;

        while (index >= 0)
        {
            var currentChar = output[index];

            if (inString)
            {
                if (currentChar == '"' && !escapeNext)
                {
                    inString = false;
                }
                else
                {
                    escapeNext = currentChar == '\\' && !escapeNext;
                }
            }
            else
            {
                if (currentChar == '"')
                {
                    inString = true;
                    escapeNext = false;
                }
                else if (currentChar is ']' or '}')
                {
                    bracketCount++;
                }
                else if (currentChar is '[' or '{')
                {
                    bracketCount--;
                    if (bracketCount == 0 && currentChar == '[')
                    {
                        break;
                    }
                }
            }

            index--;
        }

        if (index < 0)
        {
            return null;
        }

        var candidate = output[index..].Trim();

        try
        {
            _ = JArray.Parse(candidate);
            return candidate;
        }
        catch
        {
            return null;
        }
    }
}
