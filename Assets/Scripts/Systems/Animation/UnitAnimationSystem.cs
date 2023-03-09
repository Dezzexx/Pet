using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UnitAnimationSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<AnimationSwitchEvent, PlayerAnimator>, Exc<Dead>> _animationSwitchEventFilter = default;
        readonly EcsPoolInject<PlayerAnimator> _animatorPool = default;   
        readonly EcsPoolInject<AnimationSwitchEvent> _animationSwitchEventPool = default;

        public void Run (IEcsSystems systems) {
            foreach (var animationSwitchEventEntity in _animationSwitchEventFilter.Value) {
                ref var animatorComp = ref _animatorPool.Value.Get(animationSwitchEventEntity);
                ref var animationSwitchEventComp = ref _animationSwitchEventPool.Value.Get(animationSwitchEventEntity);
                // Debug.Log("зашел в анимации");
                switch (animationSwitchEventComp.AnimationSwitcher)
                {
                    case AnimationSwitchEvent.AnimationType.Idle:
                        // animatorComp.UnityAnimator.SetBool("ToIdle", true);
                        // animatorComp.UnityAnimator.SetBool("ToRun", false);
                        // animatorComp.UnityAnimator.SetBool("ToHarvest", false);
                        animatorComp.UnityAnimator.Play("Idle");
                        break;

                    case AnimationSwitchEvent.AnimationType.Run:
                        // animatorComp.UnityAnimator.SetBool("ToRun", true);
                        // animatorComp.UnityAnimator.SetBool("ToIdle", false);
                        // animatorComp.UnityAnimator.SetBool("ToHarvest", false);
                        animatorComp.UnityAnimator.Play("Run");
                        break;

                    case AnimationSwitchEvent.AnimationType.Harvest:
                        // animatorComp.UnityAnimator.SetBool("ToHarvest", true);
                        // animatorComp.UnityAnimator.SetBool("ToIdle", false);
                        // animatorComp.UnityAnimator.SetBool("ToRun", false);
                        animatorComp.UnityAnimator.Play("Harvest");
                        break;

                    default:
                        break;
                }
                _animationSwitchEventPool.Value.Del(animationSwitchEventEntity);
            }
        }
    }
}