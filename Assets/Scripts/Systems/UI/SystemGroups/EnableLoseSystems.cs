using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class EnableLoseSystems : IEcsRunSystem
    {
        private EcsSharedInject<GameState> _state = default;
        private EcsFilterInject<Inc<EnableLoseSystemsEvent>> _filter = default;
        readonly EcsPoolInject<LoseEvent> _loseEventPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _state.Value.GameMode = GameMode.lose;

                _loseEventPool.Value.Add(_state.Value.EcsWorld.NewEntity());

                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}