using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Client {
    sealed class InputInit : IEcsInitSystem {
        readonly EcsPoolInject<InputComponent> _inputPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (IEcsSystems systems) {
            var inputentity = _world.Value.NewEntity();
            ref var inputComp = ref _inputPool.Value.Add(inputentity);
            inputComp.FloatingJoystick = GameObject.FindObjectOfType<FloatingJoystick>();
            _state.Value.InputEntity = inputentity;

            Input.multiTouchEnabled = false;

            EnhancedTouchSupport.Enable();
            TouchSimulation.Enable();
        }
    }
}