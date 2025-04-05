using Atomic.Elements;

namespace Game.Scripts.UI.Presenters
{
    public interface IGameInfoViewPresenter
    {
        IAtomicValueObservable<string> BulletsLeft { get; }
        IAtomicValueObservable<string> HitPoints { get; }
        IAtomicValueObservable<string> KillCount { get; }
    }
}