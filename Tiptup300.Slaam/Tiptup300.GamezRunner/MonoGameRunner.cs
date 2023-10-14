﻿using Microsoft.Xna.Framework;
using Tiptup300.Slaam.Library.Graphics;

namespace Tiptup300.Slaam;

public class MonoGameRunner
{
   private readonly IGraphicsConfigurer _graphicsConfigurer;
   private readonly Game _game;
   private readonly IGraphicsStateService _graphicsStateService;

   public MonoGameRunner
   (
      Game game,
      IGraphicsConfigurer graphicsConfigurer,
      IGraphicsStateService graphicsStateService
   )
   {
      _game = game;
      _graphicsConfigurer = graphicsConfigurer;
      _graphicsStateService = graphicsStateService;
   }

   public void Run()
   {
      _graphicsStateService.Set(new GraphicsDeviceManager(_game));
      _graphicsConfigurer.ConfigureGraphics();

      _game.Run();
   }
}
