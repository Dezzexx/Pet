using UnityEngine;

namespace Client
{
    struct AudioSourceComponent
    {
        public AudioBehaviourMB AudioBehaviourMB;
        public UnityEngine.AudioSource UIAudioSource;
        public UnityEngine.AudioSource PlayerAudioSource;
        public UnityEngine.AudioSource EnemyAudioSource;
        public UnityEngine.AudioSource EnvironmentAudioSource;
    }
}