namespace GameSimulator.Interfaces;

public interface ISaveable
{
    bool HasSavedState { get; }
    void Save();
    void Load();
}