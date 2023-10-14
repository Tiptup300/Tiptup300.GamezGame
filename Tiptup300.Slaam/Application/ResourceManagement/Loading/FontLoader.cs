using Microsoft.Xna.Framework.Graphics;
using Tiptup300.Slaam.GamezGame;
using Tiptup300.Slaam.Library.ResourceManagement;

namespace Tiptup300.Slaam.GamezGame.ResourceManagement.Loading;

public class FontLoader : IFileLoader<SpriteFont>
{
    public object Load(string filePath)
    {
        SpriteFont output;

        output = GamerzMonoGame.Content.Load<SpriteFont>(filePath);

        return output;
    }
}
