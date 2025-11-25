namespace GameManagement
{
    public interface ISaveLoad
    {
        public void Save(GameDataStorage dataStorage);

        public void Load(GameDataStorage dataStorage);

        public void LoadDefault();
    }
}