using SaveLoadSystem;

namespace UI.PauseMenu.SaveLoad
{
    public class SaveLoadModel
    {
        private SaveLoadService _saveLoadService = SaveLoadService.Instance;
        private GameManager.GameManager _gameManager = GameManager.GameManager.Instance;

        public void Save()
        {
            _saveLoadService.SavePlayerData();
        }

        public void Load(string saveFileName)
        {
            _gameManager.LoadGame(saveFileName);
        }
    }
}