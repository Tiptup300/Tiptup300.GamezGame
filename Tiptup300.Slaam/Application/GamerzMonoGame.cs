using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Tiptup300.StateManagement;
using Tiptup300.Slaam.GamezGame.PlayerProfiles;

namespace Tiptup300.Slaam.GamezGame;

/// <summary>
/// This is the main type for your game
/// </summary>
public class GamerzMonoGame : Game
{
   new public static ContentManager Content;

   private IState _state = new SlaamGameState();


   public GamerzMonoGame(
   )
   {
   }


   protected override void Initialize()
   {
      // configuring MonoGame specific variable.
      IsFixedTimeStep = false;


      // this needs to be moved out of initialization.
      // Not sure what Content Manager is actully doing or if it is needed?
      Content = new ContentManager(Services);
      _logger.Initialize();
      _inputService.Initialize();
      _renderService.Initialize();
      _fpsRenderer.Initialize();

      base.Initialize();
   }

   protected override void LoadContent()
   {
      _resources.LoadAll();
      _renderService.LoadContent();
      _fpsRenderer.LoadContent();
      ProfileManager.Instance.LoadProfiles();

      base.LoadContent();
   }

   protected override void Update(GameTime gameTime)
   {
      if (_state is null)
      {
         _state = _gameStartRequestResolver.Resolve(new StartGameStateRequest());
      }
      _frameTimeService.AddUpdate(gameTime);
      _inputService.Update();

      _renderService.Update();
      _fpsRenderer.Update();

      // update the state using the state performer.
      //   _state = _statePerformer.Resolve(_state;// update state



      base.Update(gameTime);
   }
   protected override void Draw(GameTime gameTime)
   {
      _frameTimeService.AddDraw(gameTime);

      GraphicsDevice.Clear(Color.Black);

      _renderService.RenderRectangle(new Rectangle(0, 0, 64, 64), Color.Red);
      _renderService.Render((sb) =>
      {
         sb.Draw()


      });

      // draw the state using the state renderer.
      // where in the world is the state renderer?
      // _screenDirector.Draw(gamebatch);

      _renderService.Draw();
      _fpsRenderer.Draw();

      base.Draw(gameTime);
   }
}