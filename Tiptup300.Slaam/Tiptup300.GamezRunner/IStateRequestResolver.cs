using System.Tiptup300.StateManagement;

namespace Tiptup300.Slaam;

public interface IStateRequestResolver<T>
{
   public IState Resolve();
}
public class Tickable : IDisposable
{
   public string Name = new Func<string>(() => "dog")();
}