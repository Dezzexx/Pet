using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System;

namespace Client
{
    sealed class EnableWinSystems : IEcsRunSystem
    {
        readonly EcsSharedInject<GameState> _state = default;
        readonly EcsFilterInject<Inc<EnableWinSystemsEvent>> _filter = default;
        readonly EcsPoolInject<WinEvent> _winEventPool = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {                
                _state.Value.GameMode = GameMode.win;

                _winEventPool.Value.Add(_state.Value.EcsWorld.NewEntity());

                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}