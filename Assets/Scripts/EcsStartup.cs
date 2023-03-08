using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Client
{
    public sealed class EcsStartup : MonoBehaviour
    {
        EcsSystems _preInitSystems, _initSystems, _runSystems, _beforePlaySystems, _playSystems, _winSystems, _loseSystems;
        public EcsWorld World;
#if UNITY_EDITOR
        [SerializeField]
#endif
        private GameState _gameState;

#region Masks
        
#endregion

#region AllPools
        public AllPools AllPools;
#endregion

#region Configs
        [field: SerializeField] public GameConfig GameConfig { private set; get; }
        [field: SerializeField] public SoundConfig SoundConfig { private set; get; }
        [field: SerializeField] public PlayerConfig PlayerConfig { private set; get; }
        [field: SerializeField] public InterfaceConfig InterfaceConfig { private set; get; }
#endregion

        void Start () 
        {
            Application.targetFrameRate = 60;
            World = new EcsWorld();
            GameState.Clear();
            _gameState = GameState.Initialize(this);
            _gameState.EventsBus = new EventsBus();
            _gameState.GameMode = GameMode.beforePlay;

            _preInitSystems = new EcsSystems(World, _gameState);
            _initSystems = new EcsSystems(World, _gameState);
            _runSystems = new EcsSystems(World, _gameState);
            _beforePlaySystems = new EcsSystems(World, _gameState);
            _playSystems = new EcsSystems(World, _gameState);
            _winSystems = new EcsSystems(World, _gameState);
            _loseSystems = new EcsSystems(World, _gameState);

            // _preInitSystems
            // 
            // ;

            _initSystems
                .Add(new InputInit())
                .Add(new InitInterface())
                .Add(new InitSounds())
                .Add(new InitPools())
                .Add(new VibrationInit())

                .Add(new PlayerInit())
                
                .Add(new InitCamera())
            ;

            _runSystems
                .Add(new EnableBeforePlaySystems())
                .Add(new EnablePlaySystems())
                .Add(new EnableWinSystems())
                .Add(new EnableLoseSystems())

                .Add(new UpdateUISystem())
            ;

            _beforePlaySystems  
                // .Add(new CreditsSystem())
                .Add(new SoundSystem())
                .Add(new InputSystem())
                .Add(new VibrationSystem())

                .Add(new CreateEffectEventSystem())         
            ;

            _playSystems
                .Add(new SoundSystem())
                .Add(new VibrationSystem())
                // .Add(new CreditsSystem())

                .Add(new PlayerMoveSystem())

                .Add(new GamePauseSystem())

                // .Add(_gameState.EventsBus.GetDestroyEventsSystem()
                //     .IncSingleton<TestEvent>()
                // )
            ;

            _winSystems
                .Add(new WinSystem())
                // .Add(new CreditsSystem())
                .Add(new SoundSystem())
            ;

            _loseSystems
                .Add(new LoseSystem())
                // .Add(new CreditsSystem())
                .Add(new SoundSystem())
            ;

            _beforePlaySystems
                .DelHere<CreditsEvent>()
                .DelHere<ClickEvent>()
                .DelHere<FailClickEvent>()
                .DelHere<BuyEvent>()
                ;

            _playSystems
                .DelHere<CreditsEvent>()
                .DelHere<ClickEvent>()
                .DelHere<FailClickEvent>()
                .DelHere<BuyEvent>()
                ;

            _winSystems
                .DelHere<CreditsEvent>()
                .DelHere<ClickEvent>()
                .DelHere<FailClickEvent>()
                .DelHere<BuyEvent>()
                ;
            
            _loseSystems
                .DelHere<CreditsEvent>()
                .DelHere<ClickEvent>()
                .DelHere<FailClickEvent>()
                .DelHere<BuyEvent>()
                ;



#if UNITY_EDITOR
            _initSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(null, true, "0"));
#endif

            InjectAllSystems(_preInitSystems, _initSystems, _runSystems, _beforePlaySystems, _playSystems, _winSystems, _loseSystems);
            InitAllSystems(_preInitSystems, _initSystems, _runSystems, _beforePlaySystems, _playSystems, _winSystems, _loseSystems);
        }
        private void InjectAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Inject();
            }
        }

        private void InitAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Init();
            }
        }

        void Update () {
            _preInitSystems?.Run();
            _initSystems?.Run();
            _runSystems?.Run();
            if (_gameState.GameMode.HasFlag(GameMode.beforePlay)) _beforePlaySystems?.Run();
            if (_gameState.GameMode.HasFlag(GameMode.play)) _playSystems?.Run();
            if (_gameState.GameMode.HasFlag(GameMode.win)) _winSystems?.Run();
            if (_gameState.GameMode.HasFlag(GameMode.lose)) _loseSystems?.Run();
        }
        private void OnApplicationQuit()
        {
            _gameState.Save();
        }

        void OnDestroy () {
            if (_preInitSystems != null)
            {
                _preInitSystems.Destroy();
                _preInitSystems = null;
            }

            if (_initSystems !=  null)
            {
                _initSystems.Destroy();
                _initSystems.GetWorld().Destroy();
                _initSystems = null;
            }

            if (_runSystems !=  null)
            {
                _runSystems.Destroy();
                _runSystems = null;
            }

            if (_beforePlaySystems != null)
            {
                _beforePlaySystems.Destroy();
                _beforePlaySystems = null;
            }

            if (_playSystems != null)
            {
                _playSystems.Destroy();
                _playSystems = null;
            }

            if (_winSystems != null)
            {
                _winSystems.Destroy();
                _winSystems = null;
            }

            if (_loseSystems !=  null)
            {
                _loseSystems.Destroy();
                _loseSystems = null;
            }
            _gameState.EventsBus.Destroy();
        }
    }
}