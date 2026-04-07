namespace EntityConfiguration.Extensions;

public static class StringExtensions
{
    extension(string value)
    {
        public string ToCamelCase()
        {
            if (string.IsNullOrEmpty(value) || char.IsLower(value[0]))
                return value;

            return char.ToLowerInvariant(value[0]) + value[1..];
        }
    }
}
