using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerMoveSystem : IEcsRunSystem {    
        readonly EcsFilterInject<Inc<TouchComponent, InputComponent>, Exc<DisableInputComponent>> _touchFilter = default;
        readonly EcsPoolInject<TouchComponent> _touchPool = default;
        readonly EcsPoolInject<InputComponent> _inputPool = default;
        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<Player> _playerPool = default;
        readonly EcsPoolInject<AnimationSwitchEvent> _animationSwitchEvent = default;

        readonly EcsSharedInject<GameState> _state = default;

        private Vector3 _direction;
        private float _smoothForLookAt = 20f;
        private bool _notRunning;

        public void Run (IEcsSystems systems) {
            foreach (var touchEntity in _touchFilter.Value) {
                ref var touchComp = ref _touchPool.Value.Get(touchEntity);
                ref var inputComp = ref _inputPool.Value.Get(touchEntity);
                ref var playerViewComp = ref _viewPool.Value.Get(_state.Value.PlayerEntity);
                ref var playerComp = ref _playerPool.Value.Get(_state.Value.PlayerEntity);

                _direction = new Vector3(inputComp.FloatingJoystick.Direction.normalized.x, 0f, inputComp.FloatingJoystick.Direction.normalized.y);
                playerViewComp.Transform.position += _direction * Time.deltaTime * playerComp.Speed;
                playerViewComp.Transform.LookAt(playerViewComp.Transform.position + _direction * _smoothForLookAt);

                switch (touchComp.Phase)
                {
                    case TouchPhase.Began:
                        _notRunning = true;
                        break;

                    case TouchPhase.Moved:
                        if (_notRunning) {
                            _animationSwitchEvent.Value.Add(_state.Value.PlayerEntity).AnimationSwitcher = AnimationSwitchEvent.AnimationType.Run;
                            _notRunning = false;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (!_notRunning) {
                            _animationSwitchEvent.Value.Add(_state.Value.PlayerEntity).AnimationSwitcher = AnimationSwitchEvent.AnimationType.Idle;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}