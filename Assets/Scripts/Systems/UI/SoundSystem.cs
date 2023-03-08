using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SoundSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _world = default;
        readonly EcsSharedInject<GameState> _state = default;

        readonly EcsFilterInject<Inc<SoundEvent>> _filter = default;

        readonly EcsPoolInject<AudioSourceComponent> _sourcePool = default;
        readonly EcsPoolInject<SoundEvent> _soundPool = default;
        readonly EcsPoolInject<VibrationEvent> _vibrationPool = default;
        readonly EcsPoolInject<TargetAudioSource> _targetAudioSourcePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var sourceComp = ref _sourcePool.Value.Get(_state.Value.SoundsEntity);
                ref var soundComp = ref _soundPool.Value.Get(entity);
                var soundConfig = _state.Value.SoundConfig;

                switch (soundComp.Sound)
                {
                    case SoundEvent.SoundValue.Click:
                        sourceComp.UIAudioSource.PlayOneShot(soundConfig.ClickSound);
                        LightVibration();
                        break;

                    case SoundEvent.SoundValue.BuyClick:
                        sourceComp.UIAudioSource.PlayOneShot(soundConfig.BuySound);
                        MediumVibration();
                        break;

                    case SoundEvent.SoundValue.FailClick:
                        sourceComp.UIAudioSource.PlayOneShot(soundConfig.FailClickSound);
                        LightVibration();
                        break;

                    case SoundEvent.SoundValue.EnableEngine:
                        PlaySound(entity);
                        break;

                    case SoundEvent.SoundValue.DisableEngine:
                        StopSound(entity);
                        break;
                }
                _soundPool.Value.Del(entity);
            }
        }

        private void LightVibration()
        {
            ref var vibrationComp = ref _vibrationPool.Value.Add(_world.Value.NewEntity());
            vibrationComp.Vibration = VibrationEvent.VibrationType.LightImpack;
        }

        private void MediumVibration()
        {
            ref var vibrationComp = ref _vibrationPool.Value.Add(_world.Value.NewEntity());
            vibrationComp.Vibration = VibrationEvent.VibrationType.MediumImpact;
        }

        private void HardVibration()
        {
            ref var vibrationComp = ref _vibrationPool.Value.Add(_world.Value.NewEntity());
            vibrationComp.Vibration = VibrationEvent.VibrationType.HeavyImpact;
        }

        private void PlaySound(int entity)
        {
            ref var sourceComp = ref _targetAudioSourcePool.Value.Get(entity);
            sourceComp.ConstantSource.pitch = Random.Range(0.9f, 1.1f);
            sourceComp.ConstantSource.Play();
        }
        private void PlayEventOneShot(int entity, AudioClip sound)
        {
            ref var sourceComp = ref _targetAudioSourcePool.Value.Get(entity);
            sourceComp.EventSource.pitch = Random.Range(0.9f, 1.1f);
            sourceComp.EventSource.PlayOneShot(sound);
        }

        private void StopSound(int entity)
        {
            ref var sourceComp = ref _targetAudioSourcePool.Value.Get(entity);
            sourceComp.ConstantSource.Stop();
        }

    }
}