namespace Tiptup300.Gamez.State;

public interface IState : IDisposable
{
   IState? Parent {get;}
   IEnumerable<IState>? Children {get;}

   

   public void RemoveChild(IState state)
   {
      if(Children is null)
         throw new Exception();

      this.Children.Remove(state);
   }
   
   public void Dispose()
   {
      if(Parent is not null)
         this.Parent.RemoveChild(this);

      if(Children is not null)
         _clearChildren();

      base.Dispose();
   }

   public void ReplaceWith(IState newState)
   {
      this.Parent.AddChild(newState);
      this.Parent.RemoveChild(this);
      Dispose();
   }

   private void _clearChildren()
   {
      foreach(var x in Children)
         x.Dispose();
   }
}
public static class StateExtensions 
{
}
public interface IBaseState : IState
{
   
}

----------------------------

namespace Tiptup300.Gamez.Ticks;

public struct TickFrame
{
   public int FrameNumber {get; private set;}
   public float Delta {get; private set;}
}

//-------------------

namespace Tiptup300.GamezGame.Scenes.LogoScene;

public record LogoSceneStateBuilderRequest 
(
   bool ShowHiddenLogo = false
);
public class LogoSceneStateBuilder : MagicService
   : StateBuilder<LogoSceneState, LogoSceneStateBuilderRequest>
{
   public LogoSceneState BuildState(LogoSceneStateBuilderRequest request)
   {
      LogoSceneState output;

      LogoSceneAssets.LoadAssets(_resourceService);
      var drawRect = _renderService.GetViewpointRect();
      float x = (drawRect.Width / 2f) - (LogoSceneAssets.LogoTexture.Width/2f);
      float y = request.ShowHiddenLogo ? 0 : (drawRect.Height / 2f) - (LogoSceneAssets.LogoTexture.Height/2f);

      output = new LogoSceneState(
         Parent: state.Parent,
         Position: new Vector2D(
            x: x,
            y: y
         )
      );

      drawRect.Dispose();
      drawRect = null;

      request.Dispose();
      request = null;

      return output;
   }
}

public class LogoSceneAssets : MagicStateService
{
   public static Resource<Texture2D> LogoTexture;
   private static const string LOGOTEXTURE_ASSETID 
      = "Tiptup300.GamezGame.Assets.LogoScene.LogoTexture2D";

   public static Resource<Texture2D> BackgroundTexture;
   private static const string BACKGROUNDTEXTURE_ASSETID 
      = "Tiptup300.GamezGame.Assets.LogoScene.BackgroundTexture2D";

   private readonly IResourceService _resourceService;

   public LogoSceneAssets
   (
      IResourcesService resourcesService
   )
   {
      _resourcesService = resourcesService;
   }

   public void Load() 
   {
      if(LogoTexture is null || LogoTexture.Disposed)
      {
         LogoTexture = _resourceService.LoadAsync<Texture2D>(
            assetId: LogoSceneAssets.LOGO_TEXTURE_ASSETID
         );
      }
      
      if(BackgroundTexture is null || BackgroundTexture.Disposed)
      {
         BackgroundTexture = _resourceService.LoadAsync<Texture2D>(
            assetId: LogoSceneAssets.BACKGROUNDTEXTURE_ASSETID
         );  
      }
   }

   public void Unload()
   {
      LogoTexture.Dispose();
      LogoTexture = null;

      BackgroundTexture.Dispose();
      BackgroundTexture = null;
   }

}

public struct MainMenuSceneState : IState
{
   
}
public struct LogoSceneState : IState
{
   public IState Parent {get; private set;}
   public IState[] Children {get; private set;} = null;

   public enum Substate 
   {
      MovingDown
      FadingOut,
      FadedOut
   }
   public Substate State {get; set;}
   public Vector2 MovingDown_Position {get; set;}
   public float FadingOut_Opacity {get; set;}
   public MainMenuSceneState FadedOut_MainMenuSceneState {get; set;}


   public override Dispose()
   {
      Parent = null;
      State = null;
      MovingDown_Position = null;
      FadedOut_MainMenuSceneState = null;
      
      base.Dispose();
   }
}
public class LogoSceneStateTicker__Service : ISceneStateTicker<LogoSceneState>, IStateRenderer<LogoSceneState>
{
   private static const TICK_RATE = 1 / 60f;
   private static const RENDER_RATE = TICK_RATE;

   private readonly LogoSceneAssets__StateService _assets;
   private readonly IStateBuilder<MainMenuSceneState, MainMenuSceneStateBuilderRequest> _mainMenuSceneStateBuilder;

   public LogoSceneStateTicker
   (
      LogoSceneAssets__StateService assets,
      IStateBuilder<MainMenuSceneState, MainMenuSceneStateBuilderRequest> mainMenuSceneStateBuilder
   )
   {
      _assets = assets;
      _mainMenuSceneStateBuilder = mainMenuSceneStateBuilder;
   }

   public void Initialize()
   {
      _tickService.RequestTick(ticker: this, tickRate: TICK_RATE);
      _renderService.RequestRender(renderer: this, renderRate: RENDER_RATE);
   }

   public void TickState(
      FrameTick frameTick, 
      IBaseState baseState, 
      LogoSceneState state
   )
   {
      switch(state.State) 
      {
         case LogoSceneState.State.MovingDown:
            state.MovingDown_Position.Y += 0.25f * frameTick.Delta;
            if(state.MovingDown_Position.Y > 40f)
            {
               state.MovingDown_Position.Y = 40f;
               state.State = LogoSceneState.State.FadingOut;
            }
            break;
         case LogoSceneState.State.FadingOut:
            state.FadingOut_Opacity -= 0.002f * frameTick.Delta;
            if(state.FadingOut_Opacity <= 0)
            {
               state.FadingOut_Opacity = 0f;
               state.Substate = LogoSceneState.State.FadedOut;

               FadedOut_MainMenuSceneState = _mainMenuSceneStateBuilder.BuildState(
                  new MainMenuSceneStateBuilderRequest()
               );
               state.AddSiblingState(
                  FadedOut_MainMenuSceneState
               );
               LogoSceneAssets.Unload();
            }
            break;
         case LogoSceneState.State.FadedOut:
            if(state.FadedOut_MainMenuSceneState.IsLoaded)
            {
               state.Remove();
               _stateService.RemoveState(state);
            }
            break;
      }
   }

   public void RenderState(FrameTick frameTick, LogoSceneState state)
   {
      _renderService.RenderTexture(LogoSceneAssets.LogoTexture, )
   }
}
/*
-------------------------

-----------------------

but what about transistions 

IEnumerable

coroutines

yield (tickState, TICK_RATE;

something has to determine 

The main thing here on my mind is mostly the concept in Mario 64, where there's the abilty to do a list of IEnumerable returns, 

So you can do an array of actions really easily.

Maybe this is fundamentally incompatiable with my concept.

something has to be discussed about multiple scenes

are they scenes? Scene? 

And how do I have parallel scenes.

_sceneService.AddParallelScene
(
   new MenuScreenTickState(
      
   )
);

_sceneService.AddNextScene<>
(
   new GameCreditsTickState(

   )
);
_sceneService.ReplaceParallelScene
(

);
_sceneService.ReplaceNextScene
(

);
_sceneService.QueueScene
(

);



*/