using SimpleInjector;

namespace Tiptup300.Slaam;

static class Program
{
   public static void Main(
      string[] args
   )
   {
      var serviceProvider = build(
         (container) =>
         {

         }
      );
      var gameRunner = serviceProvider.GetService(typeof(MonoGameRunner)) as MonoGameRunner;
      if (gameRunner is null) return;
      gameRunner.Run();

   }

   public static IServiceProvider build(Action<IServiceProvider> registerServicesFunc)
   {
      var container = new Container();

      container.RegisterInstance<IServiceProvider>(container);

      registerServicesFunc.Invoke(container);

      container.Verify();

      return container;
   }

}

