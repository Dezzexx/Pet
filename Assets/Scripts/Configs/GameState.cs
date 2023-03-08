using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Saver;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// using SevenBoldPencil.EasyEvents;
using UnityEngine.SceneManagement;

namespace Client
{
#if UNITY_EDITOR
    [System.Serializable]
#endif
    public class GameState
    {
        private static GameState _gameState = null;

        public EcsSystems EcsSystems;
        public EcsWorld EcsWorld;
        public EventsBus EventsBus;

        public GameMode GameMode;

#region Input;
        public GraphicRaycaster Raycaster;
        public EventSystem EventSystem;
#endregion

#region Configs
        public GameConfig GameConfig { private set; get; }
        public SoundConfig SoundConfig { private set; get; }
        public PlayerConfig PlayerConfig { private set; get; }
        public InterfaceConfig InterfaceConfig { private set; get; }
        #endregion

        public AllPools AllPools;
        public AllPools ActivePools;

        public bool Sounds = true;
        public bool Music = true;
        public bool Vibration = true;

        public int PlayerResourceValue;
        public int SceneNumber;

#region entity            
        public int LastSceneNumber;
        public int InterfaceEntity;
        public int InputEntity;   
        public int SoundsEntity;          
        public int VibrationEntity;                 
        public int CameraEntity;
        public int PlayerEntity;
        public const int NULL_ENTITY = -1;
#endregion

#region Other
        public int TutorialStage;
        public int LevelProgressIndex;
#endregion

        private GameState(in EcsStartup ecsStartup)
        {
            EcsWorld = ecsStartup.World;
            AllPools = ecsStartup.AllPools;
            SoundConfig = ecsStartup.SoundConfig;
            GameConfig = ecsStartup.GameConfig;
            PlayerConfig = ecsStartup.PlayerConfig;
            InterfaceConfig = ecsStartup.InterfaceConfig;

            LastSceneNumber = SceneManager.sceneCountInBuildSettings;

            Load();
        }

        public static void Clear()
        {
            _gameState = null;
        }

        public static GameState Initialize(in EcsStartup ecsStartup)
        {
            if (_gameState is null)
            {
                _gameState = new GameState(in ecsStartup);
            }
            
            return _gameState;
        }

        public static GameState Get()
        {
            return _gameState;
        }

        #region Save
        public void Load()
        {
            var saver = new JsonSaver();
            SavedData data = saver.Load();

            Sounds = data.Sounds;
            Music = data.Music;
            Vibration = data.Vibration;
            PlayerResourceValue = data.PlayerResourceValue;
            SceneNumber = data.SceneNumber;
            TutorialStage = data.TutorialStage;
            LevelProgressIndex = data.LevelProgressIndex;

            Debug.Log("Load");
        }

        public void Save()
        {
            SavedData data = new SavedData();
            data.Sounds = Sounds;
            data.Music = Music;
            data.Vibration = Vibration;
            data.PlayerResourceValue = PlayerResourceValue;
            data.SceneNumber = SceneNumber;
            data.TutorialStage = TutorialStage;
            data.LevelProgressIndex = LevelProgressIndex;        

            var saver = new JsonSaver();
            saver.Save(data);
            Debug.Log("Save");
        }
        #endregion Save

        public void LoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }

    public enum GameMode { beforePlay, play, win, lose }
}