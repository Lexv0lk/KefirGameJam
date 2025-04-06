using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Audio Settings", menuName = "Configs/Audio")]
    public class AudioConfig : ScriptableObject
    {
        public float MusicVolume = 1;
        public float SoundVolume = 1;
    }
}