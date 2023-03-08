using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class InitCamera : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<CameraComponent> _cameraComponentPool = default;
        
        public void Init (IEcsSystems systems) {
            Camera camera = Camera.main;

            int entity = _world.Value.NewEntity();
            ref var CameraComponent = ref _cameraComponentPool.Value.Add(entity);
            CameraComponent.Camera = camera;
            CameraComponent.Transform = camera.transform;

            _state.Value.CameraEntity = entity;
        }
    }
}