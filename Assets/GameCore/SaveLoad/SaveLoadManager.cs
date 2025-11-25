using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameManagement
{
    public class SaveLoadManager
    {
        private readonly List<ISaveLoad> _saveLoads;

        private readonly GameDataStorage _dataStorage;

        private string _path = Path.Combine(Application.persistentDataPath, "playerData.json");

        public SaveLoadManager(IEnumerable<ISaveLoad> saveLoads)
        {
            _saveLoads = new List<ISaveLoad>(saveLoads);

            _dataStorage = new();
        }

        public void Load()
        {
            Debug.Log($"load from {_path}");

            if (File.Exists(_path))
            {
                var stringData = File.ReadAllText(_path);

                _dataStorage.SetupStorage(stringData);
            }

            foreach (ISaveLoad saveLoad in _saveLoads)
            {
                saveLoad.Load(_dataStorage);

                Debug.Log($"load for {saveLoad}");
            }
        }

        public void Save()
        {
            Debug.Log($"save to {_path}");

            foreach (ISaveLoad saveLoad in _saveLoads)
            {
                saveLoad.Save(_dataStorage);

                Debug.Log($"save for {saveLoad}");
            }

            var stringData = _dataStorage.GetStorageString();

            File.WriteAllText(_path, stringData);
        }
    }
}