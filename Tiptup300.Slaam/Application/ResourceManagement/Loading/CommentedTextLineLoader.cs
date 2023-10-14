using Tiptup300.Slaam.Library.ResourceManagement;

namespace Tiptup300.Slaam.GamezGame.ResourceManagement.Loading;

public class CommentedTextLineLoader : IFileLoader<string[]>
{
    public object Load(string baseName)
    {
        return System.Text.Json.JsonSerializer.Deserialize<string[]>(File.ReadAllText(baseName));
    }
}
