using Humanizer;

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     String extensions
/// </summary>
/// <remarks>
///     This class contains extension methods for strings.
///     It includes methods for converting strings to snake case and pluralizing strings.
/// </remarks>

public static class StringExtensions
{
    /// <summary>
    ///     Convert a string to snake case
    /// </summary>
    /// <param name="text">The string to convert</param>
    /// <returns>The string converted to snake case</returns>
    public static string ToSnakeCase(this string text)
    {
        return new string(Convert(text.GetEnumerator()).ToArray());
        
        static IEnumerable<char> Convert(CharEnumerator chars)
        {
            if (!chars.MoveNext())
                yield break;

            yield return char.ToLower(chars.Current);
            while (chars.MoveNext())
            {
                if (char.IsUpper(chars.Current))
                {
                    yield return '_';
                    yield return char.ToLower(chars.Current);
                }
                else
                {
                    yield return chars.Current;
                }
            }
        }
    }
    /// <summary>
    ///     Pluralize a string
    /// </summary>
    /// <param name="text">The string to convert</param>
    /// <returns>The string converted to plural</returns>
    public static string ToPlural(this string text)
    {
        return text.Pluralize(false);
    }
}