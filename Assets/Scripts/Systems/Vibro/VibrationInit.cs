using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Lofelt.NiceVibrations;

namespace Client
{
    sealed class VibrationInit : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsPoolInject<VibrationComponent> _vibroPool = default;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var vibroEntity = world.NewEntity();
            _state.Value.VibrationEntity = vibroEntity;
            _vibroPool.Value.Add(vibroEntity);
        }
    }
}