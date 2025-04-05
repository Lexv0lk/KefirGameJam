using Atomic.Elements;

namespace Game.Scripts.Models
{
    public class KillCountModel
    {
        public AtomicVariable<int> Kills;

        public KillCountModel()
        {
            Kills = new AtomicVariable<int>(0);
        }
    }
}