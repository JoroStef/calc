// See https://aka.ms/new-console-template for more information
using Slope.Helpers;
using Slope.Models;
using Slope.Report;
using Slope.Report.Models;

Console.WriteLine("Hello, World!");

var slopeGeometry = new SlopeGeometry
    (
        pA: new Point2D(-1.55, 3.1)
    );

var soil = new SoilLayer
    (
        UnitWeight: 19,
        FrictionAngle: 17.8,
        Cochesion: 13.8
    );

var actions = new List<DistributedLoad>()
{
    //// q
    //new DistributedLoad
    //    (
    //        Intensity: 7.5,
    //        xStart: -2.3,
    //        Length: double.NaN
    //    ),
    //// H1
    //new DistributedLoad
    //    (
    //        Intensity: 30.0,
    //        xStart: -2.3 - 0.5,
    //        Length: 3.00
    //    ),
    //// H2
    //new DistributedLoad
    //    (
    //        Intensity: 60.0,
    //        xStart: -8.0,
    //        Length: 10.00
    //    ),

    // Crane
    new DistributedLoad
        (
            Intensity: 100.0,
            xStart: -4.5,
            Length: 3.5
        )
};

var calcContext = new CalculationContext
{
    SlopeGeometry = slopeGeometry,
    Soil = soil,
    Actions = actions
};

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

var sortedOutputs = filteredOutputs.OrderBy(m => m.Value.FOS).Take(9);

foreach (var m in sortedOutputs)
{
    var o = m.Value;
    if (o.FOS > 0)
    {
        Console.WriteLine($"x0 = {o.SlipSurface.Centroid.X.ToString("F3")}\ty0 = {o.SlipSurface.Centroid.Y.ToString("F3")}\tFoS = {o.FOS.ToString("F3")}");
    }
}

//

var criticalOutput = minValue!;

SvgDrawing drawing = GenerateDrawing(slopeGeometry, criticalOutput);

GenerateReport(drawing);

// ---
// Helpers
// ---
static SvgDrawing GenerateDrawing(SlopeGeometry slopeGeometry, Output critivalOutput)
{
    var slipSurfaceDetails = critivalOutput.SlipSurface.GetDetails(slopeGeometry);

    var drawing = new SvgDrawing();

    drawing.Add(new SvgLine(new Point2D(0, 0), slopeGeometry.pA));
    drawing.Add(new SvgLine(new Point2D(0, 0), new Point2D(0 - slopeGeometry.pA.X, 0)));
    drawing.Add(new SvgLine(slopeGeometry.pA, new Point2D(slipSurfaceDetails.Bx, slipSurfaceDetails.By)));
    drawing.Add(new SvgArc(critivalOutput.SlipSurface.Centroid, new Point2D(0, 0), new Point2D(slipSurfaceDetails.Bx, slipSurfaceDetails.By), slipSurfaceDetails.Radius));
    return drawing;
}

static void GenerateReport(SvgDrawing drawing)
{
    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
    string filePath = Path.Combine(baseDir, "Report", "Template", "slope-template.html");

    string html = File.ReadAllText(filePath);

    html = html.Replace("{{SCHEME}}", drawing.ToSvg());

    File.WriteAllText(
        "C:\\dev\\myGitHub\\calc\\drawing\\report.html",
        html);
}