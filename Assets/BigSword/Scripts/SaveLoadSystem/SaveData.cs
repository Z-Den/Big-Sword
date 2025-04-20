using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaveLoadSystem
{
    [Serializable]
    public class SaveData
    {
        public string SaveName;
        public long saveDate;
        
        public float PlayerHealth;
        public int SceneIndex;
        public float[] PlayerPosition;
        
        public Vector3 PlayerPositionByVector => FloatToVec(PlayerPosition);
        public DateTime SaveDate => DateTime.FromBinary(saveDate);

        public SaveData(string fileName, float playerHealth, int sceneIndex, Vector3 playerPosition)
        {
            SaveName = fileName;
            PlayerHealth = playerHealth;
            SceneIndex = sceneIndex;
            PlayerPosition = VecToFloat(playerPosition);
            saveDate = (DateTime.Now).ToBinary();
        }

        public SaveData()
        {
            
        }
        
        private Vector3 FloatToVec(float[] floats)
        {
            var vec = new Vector3(floats[0], floats[1], floats[2]);
            return vec;
        }
        
        private float[] VecToFloat(Vector3 vec3)
        {
            var floatPos = new[] { vec3.x, vec3.y, vec3.z }; 
            return floatPos;
        }
    }
}