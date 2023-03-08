using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CreateEffectEventSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsSharedInject<GameState> _state;

        readonly EcsFilterInject<Inc<CreateEffectEvent>> _createEffectEventFilter = default;

        readonly EcsPoolInject<CreateEffectEvent> _createEffectEventPool = default;

        private int _eventEntity = GameState.NULL_ENTITY;

        public UnityEngine.AudioSource AudioSource;

        public void Run (IEcsSystems systems)
        {
            foreach (var eventEntity in _createEffectEventFilter.Value)
            {
                _eventEntity = eventEntity;

                ref var createEffectEventComponent = ref _createEffectEventPool.Value.Get(_eventEntity);

                switch (createEffectEventComponent.EffectType)
                {
                    // case EffectType.UnitShootEffectPool:
                    //     InvokeUnitShootEffect(ref createEffectEventComponent);
                    //     break;
                    
                    default:
                        Debug.LogWarning("Income unknown EffectType!");
                        break;
                }

                DeleteEvent();
            }
        }

        // private void InvokeUnitShootEffect(ref CreateEffectEvent createEffectEventComponent)
        // {
        //     var shootEffect = _state.Value.ActivePools.UnitShootEffectPool.GetFromPool();

        //     shootEffect.transform.position = createEffectEventComponent.SpawnPoint;
        //     shootEffect.transform.rotation = Quaternion.LookRotation(createEffectEventComponent.Direction);

        //     if (shootEffect.TryGetComponent<ParticleSystem>(out var shootEffectPatricle))
        //     {
        //         shootEffectPatricle.Play();
        //     }
        //     else
        //     {
        //         Debug.LogWarning("ShootEffect object from UnitShootEffectPool haven't ParticleSystem!");
        //     }
        //     _state.Value.ActivePools.UnitShootEffectPool.ReturnToPool(shootEffect);
        // }
        

        private void DeleteEvent()
        {
            _createEffectEventPool.Value.Del(_eventEntity);

            _eventEntity = GameState.NULL_ENTITY;
        }
    }
}