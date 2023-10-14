using System.Tiptup300.StateManagement;

namespace Tiptup300.Slaam.GamezGame;

public record SlaamGameState
(
   int DRAWING_GAME_WIDTH = 800,
   int DRAWING_GAME_HEIGHT = 480,
   bool ShowFPS = true
) : IState;