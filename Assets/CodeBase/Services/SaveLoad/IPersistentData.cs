namespace BlackBall.Services.SaveLoad
{
    public interface IPersistentData
    {
        public void LoadData(Data data);
        public void SaveData(ref Data data);
    }
}