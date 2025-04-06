using Game.Scripts.Configs.Models;
using UniRx;

namespace Game.Scripts.Growing
{
    public class GrowInfo
    {
        public FloatReactiveProperty MaturationTimeLeft { get; } = new();
        public FloatReactiveProperty GrowTimeLeft { get; } = new();
        public FloatReactiveProperty AmmoCountAccumulated { get; } = new();

        public BoolReactiveProperty IsGrowed { get; } = new();
        
        public WeaponConfig Result { get; }
        public GrowData GrowData { get; }
        
        public int MaximalAmmoCount => Result.AmmoAmount;

        public GrowInfo(WeaponConfig resultWeapon, GrowData growData)
        {
            Result = resultWeapon;
            GrowData = growData;
            
            MaturationTimeLeft.Value = growData.MaturationTime;
            GrowTimeLeft.Value = growData.GrowTime;
            AmmoCountAccumulated.Value = 0;
        }
    }
}