namespace BlackBall.Services.SaveLoad.Serialization
{
    public interface ISaveSerializer
    {
        public void Serialize<TGameData>(TGameData data, string filePath);
        public TGameData Deserialize<TGameData>(string filePath);
    }
}