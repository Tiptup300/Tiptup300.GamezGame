using Microsoft.Xna.Framework;

namespace Tiptup300.Slaam.Library.Timing;

public interface IFrameTimeService
{
   Frame GetLatestFrame();
}

public class FrameTimeService : IFrameTimeService
{
   private readonly TimeSpan ONE_SECOND = TimeSpan.FromSeconds(1);
   private readonly FrameTimeServiceState _state = new FrameTimeServiceState();
   private class FrameTimeServiceState
   {
      public Frame LatestFrame;
      public int FramesDrawn;
      public int FramesDrawnLast;
      public int FramesUpdated;
      public int FramesUpdatedLast;
      public TimeSpan CurrentTimer;
   }

   public Frame GetLatestFrame()
   {
      return _state.LatestFrame;
   }

   public void AddUpdate(GameTime gameTime)
   {
      _state.FramesUpdated++;
      _state.CurrentTimer += gameTime.ElapsedGameTime;

      if (_state.CurrentTimer >= ONE_SECOND)
      {
         _state.CurrentTimer -= ONE_SECOND;
         _state.FramesUpdatedLast = _state.FramesUpdated;
         _state.FramesDrawnLast = _state.FramesDrawn;

         _state.FramesDrawn = 0;
         _state.FramesUpdated = 0;
      }

      _state.LatestFrame = new Frame(
          dateTime: DateTime.UtcNow,
          movementFactor: gameTime.ElapsedGameTime.Milliseconds,
          fDPS: _state.FramesDrawnLast,
          framesUpdatedPerSecond: _state.FramesUpdatedLast
      );
   }

   public void AddDraw(GameTime gameTime)
   {
      _state.FramesDrawn++;
   }
}
