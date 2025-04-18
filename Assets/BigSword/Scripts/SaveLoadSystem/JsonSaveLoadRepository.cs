using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SaveLoadSystem
{
    public class JsonSaveLoadRepository : ISaveLoadRepository
    {
        public SaveData LoadDataFrom(string path)
        {
            if (!File.Exists(path)) return new SaveData();
            try
            {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch
            {
                return new SaveData();
            }
        }
        
        public void SaveDataTo(SaveData data, string path)
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);        
        }
    }
}