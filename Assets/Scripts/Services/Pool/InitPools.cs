using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Client
{
    sealed class InitPools : IEcsInitSystem
    {
        readonly EcsSharedInject<GameState> _state;

#region PoolsValueCount
        // private int _projectileUnitPool = 50;  
#endregion

        public void Init(IEcsSystems systems)
        {
            _state.Value.ActivePools = new AllPools();

            var spawnPoint = new Vector3(0, 0, 1000f);
            // _state.Value.ActivePools.FriendlyProjectilesUnitPool = new Pool(_state.Value.AllPools.FriendlyProjectilesUnitPool.Prefab, spawnPoint, _projectileUnitPool, parentName: "FriendlyProjectilesUnitPool");
            // _state.Value.ActivePools.EnemyProjectilesUnitPool = new Pool(_state.Value.AllPools.EnemyProjectilesUnitPool.Prefab, spawnPoint, _projectileUnitPool, parentName: "EnemyProjectilesUnitPool");
        }
    }
}
