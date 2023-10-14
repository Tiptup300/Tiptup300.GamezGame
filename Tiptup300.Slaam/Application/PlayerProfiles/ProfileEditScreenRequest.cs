using System.Tiptup300.Primitives;

namespace Tiptup300.Slaam.GamezGame.PlayerProfiles;

public class ProfileEditScreenRequest : IRequest
{
    public bool CreateNewProfile { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}
