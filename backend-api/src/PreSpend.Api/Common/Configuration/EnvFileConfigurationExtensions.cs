namespace PreSpend.Api.Common.Configuration;

public static class EnvFileConfigurationExtensions
{
    public static IConfigurationBuilder AddEnvFile(
        this IConfigurationBuilder builder,
        string path,
        bool optional = true)
    {
        if (!File.Exists(path))
        {
            if (optional)
            {
                return builder;
            }

            throw new FileNotFoundException("The configured .env file was not found.", path);
        }

        var values = File.ReadAllLines(path)
            .Select(ParseLine)
            .Where(static pair => pair is not null)
            .ToDictionary(
                static pair => pair!.Value.Key.Replace("__", ":", StringComparison.Ordinal),
                static pair => pair!.Value.Value,
                StringComparer.OrdinalIgnoreCase);

        return builder.AddInMemoryCollection(values);
    }

    private static KeyValuePair<string, string?>? ParseLine(string line)
    {
        var trimmed = line.Trim();

        if (trimmed.Length == 0 || trimmed.StartsWith('#'))
        {
            return null;
        }

        var separatorIndex = trimmed.IndexOf('=');

        if (separatorIndex <= 0)
        {
            return null;
        }

        var key = trimmed[..separatorIndex].Trim();
        var value = trimmed[(separatorIndex + 1)..].Trim().Trim('"');

        return new KeyValuePair<string, string?>(key, value);
    }
}
