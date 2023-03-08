namespace Saver
{
    public interface ISaver
    {
        public void Save(SavedData data);
        public SavedData Load(bool renew);
    }
}

