using System.Collections.Generic;
using System.IO;
using Units.Player;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveLoadService : MonoBehaviour
    {
        private string _saveDirectory;
        private string _saveFilePrefix = "savefile";
        private string _saveFileExtension = ".json";
        private static SaveLoadService _instance;
        private ISaveLoadRepository _repository ;
        private Player _player;
        
        public static SaveLoadService Instance => _instance;

        public void Init(Player player, ISaveLoadRepository repository)
        {
            _player = player;
            
            _instance = FindAnyObjectByType<SaveLoadService>();
            _repository = repository;
            
            _saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            if (!Directory.Exists(_saveDirectory))
            {
                Directory.CreateDirectory(_saveDirectory);
            }
        }

        public void SavePlayerData()
        {
            GetNewSavePath(out var savePath, out var fileName);
            
            var position = Vec3ToFloat(_player.transform.position);
            var health = _player.Health.CurrentHealth;
            var data = new SaveData(fileName, health,1, position);
            
            _repository.SaveDataTo(data, savePath);
        }

        public SaveData LoadPlayerData(string saveFileName)
        {
            string savePath = Path.Combine(_saveDirectory, saveFileName + _saveFileExtension);
            var json = File.ReadAllText(savePath);
            var data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        
        public List<SaveData> GetSaveFilesList()
        {
            var directoryInfo = new DirectoryInfo(_saveDirectory);
            var saveFiles = directoryInfo.GetFiles("*" + _saveFileExtension);

            var gameDataList = new List<SaveData>();
            foreach (var file in saveFiles)
            {
                var savePath = Path.Combine(_saveDirectory, file.Name);
                var json = File.ReadAllText(savePath);
                var data = JsonUtility.FromJson<SaveData>(json);
                gameDataList.Add(data);
            }

            return gameDataList;
        }

        private void GetNewSavePath(out string savePath, out string fileName)
        {
            var saveNumber = 0;
            do
            {
                saveNumber++;
                fileName = _saveFilePrefix + saveNumber;
                savePath = Path.Combine(_saveDirectory,fileName + _saveFileExtension);
            } while (File.Exists(savePath));
        }
        
        private float[] Vec3ToFloat(Vector3 vec3)
        {
            var floatPos = new[] { vec3.x, vec3.y, vec3.z }; 
            return floatPos;
        }
    }
}