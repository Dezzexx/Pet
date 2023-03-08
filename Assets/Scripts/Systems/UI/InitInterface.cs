using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client {
    sealed class InitInterface : IEcsInitSystem {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsPoolInject<InterfaceComponent> _interfacePool = default;

        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();
            _state.Value.InterfaceEntity = entity;
            ref var interfaceComp = ref _interfacePool.Value.Add(entity);

            interfaceComp.MainCanvasMB = GameObject.FindObjectOfType<MainCanvasMB>();
            interfaceComp.MainCanvasMB.Init(_world.Value, _state.Value);

            interfaceComp.PanelPermanentMB = GameObject.FindObjectOfType<PanelPermanentMB>();
            interfaceComp.PanelBeforeStartMB = GameObject.FindObjectOfType<PanelBeforeStartMB>();
            interfaceComp.PanelPlaySystemsMB = GameObject.FindObjectOfType<PanelPlaySystemsMB>();
            interfaceComp.PanelWinMB = GameObject.FindObjectOfType<PanelWinMB>();
            interfaceComp.PanelLoseMB = GameObject.FindObjectOfType<PanelLoseMB>();
            interfaceComp.PanelShopMB = GameObject.FindObjectOfType<PanelShopMB>();
            interfaceComp.PanelTutorialMB = GameObject.FindObjectOfType<PanelTutorialMB>();

            interfaceComp.PanelPermanentMB.UpdateMoneyPanel();
        }
    }
}