namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;

public static class JsonStringSanitizer
{
    public static string? SanitizeOutput(string stringWithJsonArray)
    {
        var startIndex = stringWithJsonArray.IndexOf('[');
        if (startIndex == -1)
        {
            return null; // No array found
        }

        var bracketCount = 1;
        var currentIndex = startIndex + 1;

        // Traverse the string to find the matching closing bracket
        while (bracketCount > 0 && currentIndex < stringWithJsonArray.Length)
        {
            switch (stringWithJsonArray[currentIndex])
            {
                case '[':
                    bracketCount++;
                    break;
                case ']':
                    bracketCount--;
                    break;
            }

            currentIndex++;
        }

        return bracketCount == 0
            ? stringWithJsonArray[startIndex..currentIndex]
            : null; // Unbalanced brackets
    }
}