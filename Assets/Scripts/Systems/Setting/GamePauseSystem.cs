using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class GamePauseSystem : IEcsRunSystem { 
        readonly EcsFilterInject<Inc<TimeScaleEvent>> _timeScaleFilter = default;
        readonly EcsPoolInject<TimeScaleEvent> _timeScaleEvent = default;

        public void Run (IEcsSystems systems) {
            foreach (var eventEntity in _timeScaleFilter.Value) {
                ref var timescaleComp = ref _timeScaleEvent.Value.Get(eventEntity);
                Time.timeScale = timescaleComp.TimeScale;
                _timeScaleEvent.Value.Del(eventEntity);
            }
        }
    }
}