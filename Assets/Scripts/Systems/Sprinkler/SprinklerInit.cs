using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class SprinklerInit : IEcsInitSystem {
        readonly EcsPoolInject<Sprinkler> _sprinklerPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsWorldInject _world = default;

        private float _rotationSpeed = 0.2f;

        public void Init (IEcsSystems systems) {
            var sprinklers = GameObject.FindObjectsOfType<SprinklerPlugMB>();

            foreach (var sprinkler in sprinklers) {
                var sprinklerEntity = _world.Value.NewEntity();

                ref var sprinklerComp = ref _sprinklerPool.Value.Add(sprinklerEntity);
                sprinklerComp.RotationSpeed = _rotationSpeed;

                ref var viewComp = ref _viewPool.Value.Add(sprinklerEntity);
                viewComp.Transform = sprinkler.transform;
            }
        }
    }
}