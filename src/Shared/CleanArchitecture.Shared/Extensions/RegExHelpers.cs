using System.Text.RegularExpressions;

namespace CleanArchitecture.Shared.Extensions;

public static class RegExHelpers
{
    public static bool MatchesApiVersion(string apiVersion, string text)
    {
        string pattern = $@"(?<=\/|^){Regex.Escape(apiVersion)}(?=\/|$)";

        Regex exactMatchRegex = new Regex(pattern, RegexOptions.Compiled);

        return exactMatchRegex.IsMatch(text);
    }
}