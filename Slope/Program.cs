// See https://aka.ms/new-console-template for more information
using Slope.Helpers;
using Slope.Models;

Console.WriteLine("Hello, World!");

var slopeGeometry = new SlopeGeometry
    (
        pA: new Point2D(-2.3, 4.6)
    );

var slipSurface = new SlipSurface
{
    Centroid = new Point2D(X: -0.55, Y: 10.10)
};

var soil = new SoilLayer
    (
        UnitWeight: 20, 
        FrictionAngle: 21.0, 
        Cochesion: 21.9
    );

var actions = new List<DistributedLoad>()
{
    new DistributedLoad
        (
            Intensity: 30.0,
            xStart: -2.3 - 0.5,
            Length: 3.00
        ),
    new DistributedLoad
        (
            Intensity: 60.0,
            xStart: -8.0,
            Length: 10.00
        )
};

var calcContext = new CalculationContext
{
    SlopeGeometry = slopeGeometry,
    Soil = soil,
    Actions = actions
};

var output = Procedures.CalculateFos(calcContext, slipSurface);

var outputs = Trace.Start(calcContext);

var filteredOutputs = outputs.Where(m => m.Value.FOS > 0 && m.Value.FOS < 2 && m.Value.FOS != double.NaN).ToList();

var minFosNode = filteredOutputs.First();
for (int i = 0; i < filteredOutputs.Count; i++)
{
    if (filteredOutputs[i].Value.FOS < minFosNode.Value.FOS)
    {
        minFosNode = filteredOutputs[i];
    }
}

var minValue = minFosNode.Value;
Console.WriteLine($"x0 = {minValue.SlipSurface.Centroid.X.ToString("F3")}\ty0 = {minValue.SlipSurface.Centroid.Y.ToString("F3")}\tFoS = {minValue.FOS.ToString("F3")}");
Console.WriteLine();

//Console.WriteLine($"FoS = {output.FOS}");

var sortedOutputs = filteredOutputs.OrderBy(m => m.Value.FOS).Take(9);

foreach (var m in sortedOutputs)
{
    var o = m.Value;
    if (o.FOS > 0)
    {
        Console.WriteLine($"x0 = {o.SlipSurface.Centroid.X.ToString("F3")}\ty0 = {o.SlipSurface.Centroid.Y.ToString("F3")}\tFoS = {o.FOS.ToString("F3")}");
    }
}
