using System.Tiptup300.StateManagement;

namespace Tiptup300.Slaam.GamezGame;

public class GameStartStateResolver : IStateRequestResolver<StartGameStateRequest>
{
   private readonly IState _startingState;

   public GameStartStateResolver(IState startingState)
   {
      _startingState = startingState;
   }

   public IState Resolve()
   {
      return _startingState;
   }
}
