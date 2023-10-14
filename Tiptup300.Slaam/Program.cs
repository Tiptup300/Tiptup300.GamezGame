namespace Tiptup300.Slaam;

static class Program
{
   static void Main(
      string[] args
   ) => new ServiceLocator()
             .GetService<MonoGameRunner>()
             .Run();

}

