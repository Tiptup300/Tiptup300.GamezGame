namespace Tiptup300.Slaam.Library.Timing;

public struct Frame
{
   public Frame(DateTime dateTime, float movementFactor, int fDPS, int framesUpdatedPerSecond)
   {
      Timestamp = dateTime;
      MovementFactor = movementFactor;
      FramesDrawnPerSecond = fDPS;
      FramesUpdatedPerSecond = framesUpdatedPerSecond;
   }

   public DateTime Timestamp { private set; get; }
   public float MovementFactor { private set; get; }
   public TimeSpan MovementFactorTimeSpan { get { return new TimeSpan(0, 0, 0, 0, (int)MovementFactor); } }
   public int FramesDrawnPerSecond { private set; get; }
   public int FramesUpdatedPerSecond { private set; get; }
}
