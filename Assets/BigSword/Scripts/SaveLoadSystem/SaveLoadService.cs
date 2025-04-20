using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bootstrapper;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveLoadSystem
{
    public class SaveLoadService : MonoBehaviour
    {
        private readonly string _saveFilePrefix = "savefile";
        private readonly string _saveFileExtension = ".json";
        private string _saveDirectory;
        private ISaveLoadRepository _repository ;
        private static SaveLoadService _instance;
        public static SaveLoadService Instance => _instance;

        public void Init(ISaveLoadRepository repository)
        {
            _instance = FindAnyObjectByType<SaveLoadService>();
            _repository = repository;
            
            _saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            
            if (!Directory.Exists(_saveDirectory))
                Directory.CreateDirectory(_saveDirectory);
        }

        public void SavePlayerData()
        {
            var player = FindAnyObjectByType<Player>();
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            GetNewSavePath(out var savePath, out var fileName);
            
            var position = player.transform.position;
            var health = player.Health.CurrentHealth;
            var data = new SaveData(fileName, health, currentSceneIndex, position);
            
            _repository.SaveDataTo(data, savePath);
        }

        public SaveData GetPlayerData(string saveFileName)
        {
            var savePath = Path.Combine(_saveDirectory, saveFileName + _saveFileExtension);
            return _repository.LoadDataFrom(savePath);
        }
        
        public List<SaveData> GetSaveFilesList()
        {
            var directoryInfo = new DirectoryInfo(_saveDirectory);
            var saveFiles = directoryInfo.GetFiles("*" + _saveFileExtension);

            var gameDataList = new List<SaveData>();
            foreach (var file in saveFiles)
            {
                var savePath = Path.Combine(_saveDirectory, file.Name);
                var data = _repository.LoadDataFrom(savePath);
                gameDataList.Add(data);
            }

            return gameDataList;
        }

        public SaveData GetLatestSaveData()
        {
            var saves = GetSaveFilesList();
            var max = saves.Max(entry => entry.SaveDate);
            var latestSave = (SaveData)saves.Where(entry => entry.SaveDate == max);
            Debug.Log(latestSave);
            return latestSave;
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
        

    }
}