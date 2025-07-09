using ChatbotAi.Core.Options;

namespace ChatbotAi.Core.Data.Types;

internal class DatabaseOptions : IOptions
{
    public static string SectionName => "database";
    public string ConnectionString { get; set; }
    public string Type { get; set; }
    public bool CreateIfNotExists { get; set; } = false;
}
