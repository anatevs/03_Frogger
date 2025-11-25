using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameManagement
{
    public class GameDataStorage
    {
        private Dictionary<string, string> _data = new();

        public bool TryGetData<T>(out T data)
        {
            data = default;

            if (_data.TryGetValue(GetDataName<T>(), out var dataString))
            {
                data = JsonConvert.DeserializeObject<T>(dataString);
                return true;
            }

            else
            {
                return false;
            }
        }

        public void SetData<T>(T data)
        {
            var dataString = GetDataName<T>();

            if (_data.ContainsKey(dataString))
            {
                _data.Remove(dataString);
            }

            _data.Add(dataString, JsonConvert.SerializeObject(data));
        }

        public void SetupStorage(string loadDataString)
        {
            _data = JsonConvert.DeserializeObject<Dictionary<string, string>>(loadDataString);
        }

        public string GetStorageString()
        {
            return JsonConvert.SerializeObject(_data);
        }

        private string GetDataName<T>()
        {
            return typeof(T).Name;
        }
    }
}