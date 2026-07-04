using Slope.Models;

public sealed class MenuContext
{
    public string? ProjectFolder { get; set; }

    public CalculationInput? Input { get; set; }

    public bool HasProject => Input != null;

    public bool IsRunning { get; set; } = true;
}