using SaveLoadSystem;

namespace UI.PauseMenu.SaveLoad
{
    public class SaveLoadModel
    {
        private SaveLoadService _saveLoadService = SaveLoadService.Instance;

        public void Save()
        {
            _saveLoadService.SavePlayerData();
        }
    }
}