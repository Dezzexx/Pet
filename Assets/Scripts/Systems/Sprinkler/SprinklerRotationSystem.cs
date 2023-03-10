using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class SprinklerRotationSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<Sprinkler, View>> _sprinklerFilter = default;     
        readonly EcsPoolInject<Sprinkler> _sprinklerPool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        public void Run (IEcsSystems systems) {
            foreach (var sprinklerEntity in _sprinklerFilter.Value) {
                ref var viewComp = ref _viewPool.Value.Get(sprinklerEntity);
                ref var sprinklerComp = ref _sprinklerPool.Value.Get(sprinklerEntity);

                viewComp.Transform.Rotate(0, 1 * sprinklerComp.RotationSpeed, 0);
            }
        }
    }
}