namespace SaveLoadSystem
{
    public interface ISaveLoadRepository
    {
        public SaveData LoadDataFrom(string path);
        
        public void SaveDataTo(SaveData data, string path);
    }
}