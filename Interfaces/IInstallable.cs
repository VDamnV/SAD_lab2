namespace GameSimulator.Interfaces;

public interface IInstallable
{
    bool IsInstalled { get; }
    double RequiredSpaceMB { get; }
    
    void Install(double availableSpace);
}