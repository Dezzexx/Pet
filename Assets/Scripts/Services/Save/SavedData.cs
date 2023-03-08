using System.Collections.Generic;
using UnityEngine;

namespace Saver
{
    public class SavedData
    {
        static int _version = 9;

        public static int Version
        {
            get
            {
                return _version;
            }
        }

        // -------------------------------------

        public bool Sounds = true;
        public bool Music = true;
        public bool Vibration = true;

        // -------------------------------------
        public int SceneNumber = 0;
        public int Level = 0;
        public int CurrentBiome = 1; // текущий биом
        public int TutorialStage = 0;

        // --------------------------------------

        public int PlayerResourceValue = 50;        //сколько денег у игрока
        public int LevelProgressIndex = 1;
    }
}
