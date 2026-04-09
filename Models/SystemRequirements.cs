namespace GameSimulator.Models;

public class SystemRequirements
{
    public int CpuCores { get; set; }
    public int RamMB { get; set; }
    public int GpuMemoryMB { get; set; }
    public double FreeHddSpaceMB { get; set; }

    // Метод для перевірки сумісності
    public bool MeetsRequirements(SystemRequirements target)
    {
        return CpuCores >= target.CpuCores &&
               RamMB >= target.RamMB &&
               GpuMemoryMB >= target.GpuMemoryMB;
    }
}