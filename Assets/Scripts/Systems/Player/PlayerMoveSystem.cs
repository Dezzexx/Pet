using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerMoveSystem : IEcsRunSystem {    
        readonly EcsFilterInject<Inc<TouchComponent, InputComponent>, Exc<DisableInputComponent>> _touchFilter = default;
        readonly EcsPoolInject<TouchComponent> _touchPool = default;
        readonly EcsPoolInject<InputComponent> _inputPool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        readonly EcsSharedInject<GameState> _state = default;

        public void Run (IEcsSystems systems) {
            foreach (var touchEntity in _touchFilter.Value) {
                ref var touchComp = ref _touchPool.Value.Get(touchEntity);
                ref var inputComp = ref _inputPool.Value.Get(touchEntity);
                ref var playerViewComp = ref _viewPool.Value.Get(_state.Value.PlayerEntity);

                switch (touchComp.Phase)
                {
                    case TouchPhase.Moved:
                        Vector3 direction = new Vector3(inputComp.FloatingJoystick.Direction.x, 0, inputComp.FloatingJoystick.Direction.y);
                        playerViewComp.Transform.Translate(direction * Time.deltaTime * _state.Value.PlayerConfig.Speed);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}