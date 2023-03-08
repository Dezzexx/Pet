using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
namespace Client
{
    sealed class EnablePlaySystems : IEcsRunSystem
    {
        private EcsSharedInject<GameState> _state = default;
        private EcsFilterInject<Inc<EnablePlaySystemsEvent>> _filter = default;
        

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _state.Value.GameMode = GameMode.play;

                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}