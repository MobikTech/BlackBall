using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlackBall.Services.SaveLoad.Serialization
{
    public class BinarySaveSerializer : ISaveSerializer
    {
        private readonly BinaryFormatter _binaryFormatter;

        public BinarySaveSerializer()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public void Serialize<TGameData>(TGameData data, string filePath)
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                _binaryFormatter.Serialize(fileStream, data);
            }
        }

        public TGameData Deserialize<TGameData>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("File with serialized data doesn't exist");
            
            TGameData result;
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                result = (TGameData)_binaryFormatter.Deserialize(fileStream);
            }

            return result;
        }
    }
}