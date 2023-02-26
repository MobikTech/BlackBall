using System;
using System.Collections.Generic;

namespace BlackBall.Services.SaveLoad
{
    public interface ISaveLoader
    {
        public void TryCreateSaveFile();
        public void Save(Action? callback, params IPersistentData[] persistentData);
        public void Save(Action? callback, List<IPersistentData> persistentData);

        public void Load(Action? callback, params IPersistentData[] persistentData);
        public void Load(Action? callback, List<IPersistentData> persistentData);
    }
}