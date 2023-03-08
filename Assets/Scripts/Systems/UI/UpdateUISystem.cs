using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class UpdateUISystem : IEcsRunSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<UpdateUIEvent>> _filter = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value)
            {
                ref var interfaceComp = ref _interfacePool.Value.Get(_state.Value.InterfaceEntity);

                interfaceComp.PanelPermanentMB.UpdateMoneyPanel();

                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}