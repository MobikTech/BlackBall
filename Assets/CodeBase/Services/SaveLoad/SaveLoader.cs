using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlackBall.Services.SaveLoad.Serialization;
using UnityEngine;

namespace BlackBall.Services.SaveLoad
{
    public class SaveLoader : ISaveLoader
    {
        public SaveLoader(ISaveSerializer saveSerializer)
        {
            _saveSerializer = saveSerializer;
        }

        private readonly ISaveSerializer _saveSerializer;
        private readonly string _savePath = Path.Combine(Application.persistentDataPath, "Save.dat");
        private Data _currentData;

        public void TryCreateSaveFile()
        {
            if (!File.Exists(_savePath))
                _saveSerializer.Serialize(_currentData, _savePath);
        }

        public void Save(Action? callback, params IPersistentData[] persistentData)
        {
            persistentData.ToList().ForEach(data => data.SaveData(ref _currentData));
            _saveSerializer.Serialize(_currentData, _savePath);
            callback?.Invoke();
        }

        public void Save(Action? callback, List<IPersistentData> persistentData)
        {
            persistentData.ForEach(data => data.SaveData(ref _currentData));
            _saveSerializer.Serialize(_currentData, _savePath);
            callback?.Invoke();
        }


        public void Load(Action? callback, params IPersistentData[] persistentData)
        {
            _currentData = _saveSerializer.Deserialize<Data>(_savePath);
            persistentData.ToList().ForEach(data => data.LoadData(_currentData));
            callback?.Invoke();
        }

        public void Load(Action? callback, List<IPersistentData> persistentData)
        {
            _currentData = _saveSerializer.Deserialize<Data>(_savePath);
            persistentData.ForEach(data => data.LoadData(_currentData));
            callback?.Invoke();
        }
    }
}
