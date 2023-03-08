using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class WinSystem : IEcsRunSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<WinEvent>> _filter = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;
        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                ref var interfaceComp = ref _interfacePool.Value.Get(_state.Value.InterfaceEntity);
                MainCanvasMB canvasMB = interfaceComp.MainCanvasMB;

                canvasMB.EnableWinPanel(false);
                
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}