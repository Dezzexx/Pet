using System.IO;
using UnityEngine;

namespace Saver
{
    class JsonSaver : ISaver
    {
        public void Save(SavedData data)
        {
            var path = GetPath();
            File.WriteAllText(path, JsonUtility.ToJson(data));

            SetVersion(SavedData.Version);
            SetSceneNumber(data.SceneNumber);
        }

        public SavedData Load(bool renew = false)
        {
            int prefVersion = GetVersion();
            var path = GetPath();

            // если нужно пересоздать или сохраненная версия не равна текущей или файла нет - создать новый
            if (renew || prefVersion != SavedData.Version || !File.Exists(path))
            {
                return CreateNewFile();
            }
            else
            {
                return LoadFromFile();
            }
        }

        SavedData LoadFromFile()
        {
            Debug.Log("--- LoadFromFile");
            var path = GetPath();
            var data = JsonUtility.FromJson<SavedData>(File.ReadAllText(path));
            return data;
        }

        SavedData CreateNewFile()
        {
            Debug.Log("--- CreateNewFile");
            var path = GetPath();
            var data = new SavedData();
            File.WriteAllText(path, JsonUtility.ToJson(data));
            SetVersion(SavedData.Version);
            return data;
        }

        string GetPath()
        {
            string path = "";
            //в зависимости от того где запустили игру находим путь до json файла
            #if UNITY_ANDROID && !UNITY_EDITOR
                path = Path.Combine(Application.persistentDataPath, "SaveSettings.json");
            #else
                path = Path.Combine(Application.dataPath, "SaveSettings.json");
            #endif
            return path;
        }

        int GetVersion()
        {
            return PlayerPrefs.GetInt("SaveDataVersion", 0);
        }

        void SetVersion(int v)
        {
            PlayerPrefs.SetInt("SaveDataVersion", v);
        }

        // ------------------

        public int GetSceneNumber()
        {
            return PlayerPrefs.GetInt("SceneNumber", 0);
        }

        void SetSceneNumber(int v)
        {
            PlayerPrefs.SetInt("SceneNumber", v);
        }
    }
}
