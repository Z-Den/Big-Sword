using System;

namespace SaveLoadSystem
{
    [Serializable]
    public class SaveData
    {
        public string SaveName;
        public DateTime SaveDate;
        
        public float PlayerHealth;
        public int PlayerLevel;
        public float[] PlayerPosition;

        public SaveData(string fileName, float playerHealth, int playerLevel, float[] playerPosition)
        {
            SaveName = fileName;
            PlayerHealth = playerHealth;
            PlayerLevel = playerLevel;
            PlayerPosition = playerPosition;
            SaveDate = DateTime.Now;
        }

        public SaveData()
        {
            
        }
    }
}