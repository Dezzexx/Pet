using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Client {
    sealed class InputSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<InputComponent>, Exc<DisableInputComponent>> filter = default;
        readonly EcsPoolInject<TouchComponent> _touchPool = default;

        private int _touchEntity = -1; 
        private Vector2 _initialPosition;
        private Vector2 _currentPosition;

        public void Run(IEcsSystems systems) {
            foreach (int entity in filter.Value) {
                if (Touch.activeTouches.Count is 0) continue;

                // if (!EventSystem.current.IsPointerOverGameObject())
                // {
                    _touchEntity = entity;

                    var activeTouch = Touch.activeTouches[0];
                    var phase = activeTouch.phase;

                    _currentPosition = activeTouch.screenPosition;
                    
                    switch (phase)
                    {
                        case InputTouchPhase.Began:
                            if(!_touchPool.Value.Has(_touchEntity)) _touchPool.Value.Add(_touchEntity);

                            TouchCompFilling(TouchPhase.Began, Vector3.zero);
                            break;

                        case InputTouchPhase.Moved:
                            var direction = _currentPosition - _initialPosition;

                            TouchCompFilling(TouchPhase.Moved, direction);
                            break;

                        case InputTouchPhase.Stationary:
                            TouchCompFilling(TouchPhase.Stationary, Vector3.zero);
                            break;

                        case InputTouchPhase.Ended:
                            TouchCompFilling(TouchPhase.Ended, Vector3.zero);

                            // if(_touchPool.Value.Has(_touchEntity)) _touchPool.Value.Del(_touchEntity);
                            break;
                        
                        case InputTouchPhase.Canceled:
                            TouchCompFilling(TouchPhase.Canceled, Vector3.zero);

                            // if(_touchPool.Value.Has(_touchEntity)) _touchPool.Value.Del(_touchEntity);
                            break;

                        default:
                            break;
                    }
                // }
            }
        }

        private void TouchCompFilling(TouchPhase touchPhase, Vector3 direction) {
            ref var touch = ref _touchPool.Value.Get(_touchEntity);
            touch.Phase = touchPhase;
            touch.Direction = direction;
            touch.Position = _currentPosition;

            if (touchPhase is TouchPhase.Began) {
                touch.InitialPosition = _currentPosition;
                _initialPosition = _currentPosition;
            }
        }
    }
}