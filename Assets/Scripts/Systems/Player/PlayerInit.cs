using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerInit : IEcsInitSystem {
        readonly EcsPoolInject<Player> _playerPool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsWorldInject _world = default;

        public void Init (IEcsSystems systems) {
            var playerEntity = _world.Value.NewEntity();
            _state.Value.PlayerEntity = playerEntity;

            var player = GameObject.FindObjectOfType<PlayerMB>();

            ref var playerComp = ref _playerPool.Value.Add(playerEntity);
            playerComp.PlayerMB = player;
            
            ref var viewComp = ref _viewPool.Value.Add(playerEntity);
            viewComp.Transform = player.transform;
        }
    }
}