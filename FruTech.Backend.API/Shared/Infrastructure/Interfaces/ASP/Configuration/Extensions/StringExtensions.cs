using System.Text.RegularExpressions;

namespace FruTech.Backend.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
/// <summary>
///  String extension methods
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    ///  Converts a string to kebab-case
    /// </summary>
    /// <param name="str"></param>
    public static string ToKebabCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return KebabCaseRegex().Replace(str, "-$1")
            .Trim()
            .ToLower();
    }
    
    [GeneratedRegex("(?<!ˆ)([A-Z][a-z]|(?<=[a-z])[A-Z])")]
    private static partial Regex KebabCaseRegex();
}