using Slope.Helpers;

namespace Slope.Models;

public record Output
{
    public Output()
    {
        
    }
    public Output(double fos, SlipSurface slipSurface)
    {
        this.FOS = fos;
        this.SlipSurface = slipSurface;
    }

    public double FOS { get; set; }
    public SlipSurface SlipSurface { get; set; }
}
