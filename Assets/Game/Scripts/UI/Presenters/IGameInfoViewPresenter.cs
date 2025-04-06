using Atomic.Elements;

namespace Game.Scripts.UI.Presenters
{
    public interface IGameInfoViewPresenter
    {
        IAtomicValueObservable<string> BulletsLeft { get; }
        IAtomicValueObservable<float> Health { get; }
        IAtomicValueObservable<string> KillCount { get; }
    }
}