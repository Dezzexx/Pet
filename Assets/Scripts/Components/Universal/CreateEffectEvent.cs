using UnityEngine;

namespace Client
{
    struct CreateEffectEvent
    {
        public EffectType EffectType;
        public Vector3 SpawnPoint;
        public Vector3 Direction;
        public Vector3 FinishPoint;
        public GameObject GameObjectParticle;

        /// <summary>
        /// Create some effect in current place
        /// </summary>
        /// <param name="effectType">Effect</param>
        /// <param name="spawnPoint">Spawn place</param>
        /// <param name="direction">Usually Vector3.zero</param>
        public void Invoke(EffectType effectType, Vector3 spawnPoint, Vector3 direction)
        {
            EffectType = effectType;
            SpawnPoint = spawnPoint;
            Direction = direction;
        }

        /// <summary>
        /// Create some effect in current place and move it to finish point
        /// </summary>
        /// <param name="effectType">Effect</param>
        /// <param name="spawnPoint">Spawn place</param>
        /// <param name="direction">Usually Vector3.zero</param>
        /// <param name="finishPoint">Final position for move effect</param>
        public void Invoke(EffectType effectType, Vector3 spawnPoint, Vector3 direction, Vector3 finishPoint)
        {
            EffectType = effectType;
            SpawnPoint = spawnPoint;
            Direction = direction;
            FinishPoint = finishPoint;
        }

        public void Invoke(EffectType effectType, Vector3 spawnPoint, Vector3 direction, GameObject gameObject)
        {
            EffectType = effectType;
            SpawnPoint = spawnPoint;
            Direction = direction;
            GameObjectParticle = gameObject;
        }
    }
}