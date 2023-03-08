using UnityEngine;
namespace Client
{
    struct SoundEvent
    {
        public enum SoundValue
        {
            Click, 
            BuyClick, 
            FailClick, 
            EnemySoldierShoot,
            EnemyBomberShoot,
            EnemySniperShoot,
            EnemyHeliShoot,
            EnemyTankShoot,
            DisableEngine, 
            EnableEngine,
        }
        public SoundValue Sound;
        public UnityEngine.AudioSource EventSource;
    }
}