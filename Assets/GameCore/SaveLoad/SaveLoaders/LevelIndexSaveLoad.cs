using GameCore;
using UnityEngine;

namespace GameManagement
{
    public class LevelIndexSaveLoad : 
        ISaveLoad
    {
        private readonly LevelManager _levelManager;

        private LevelIndexData _levelIndexData;

        public LevelIndexSaveLoad(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void Load(GameDataStorage dataStorage)
        {
            if (!dataStorage.TryGetData<LevelIndexData>(out _levelIndexData))
            {
                LoadDefault();
            }

            _levelManager.LevelIndex = _levelIndexData.LevelIndex;
        }

        public void LoadDefault()
        {
            _levelIndexData = new LevelIndexData(0);
        }

        public void Save(GameDataStorage dataStorage)
        {
            _levelIndexData = new LevelIndexData(_levelManager.LevelIndex);

            dataStorage.SetData<LevelIndexData>(_levelIndexData);
        }
    }

    public struct LevelIndexData
    {
        public int LevelIndex;

        public LevelIndexData(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}