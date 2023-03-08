using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class EnableBeforePlaySystems : IEcsRunSystem
    {
        private EcsSharedInject<GameState> _state = default;
        private EcsFilterInject<Inc<EnableBeforePlaySystemsEvent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _state.Value.GameMode = GameMode.beforePlay;

                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}