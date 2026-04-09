namespace GameSimulator.Models;

public class PC
{
    public bool IsConnectedToInternet { get; private set; }
    
    // Робимо Specs приватними, щоб зовнішні класи не порушували Law of Demeter
    private SystemRequirements _specs; 

    public double FreeHddSpaceMB => _specs.FreeHddSpaceMB;

    public PC(SystemRequirements specs, bool hasInternet)
    {
        _specs = specs;
        IsConnectedToInternet = hasInternet;
    }

    // Делегування: комп'ютер сам перевіряє свої вимоги
    public bool MeetsRequirements(SystemRequirements targetRequirements)
    {
        return _specs.MeetsRequirements(targetRequirements);
    }
}