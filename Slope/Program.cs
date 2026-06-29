// See https://aka.ms/new-console-template for more information
using Slope.Helpers;
using Slope.Models;
using Slope.Report;
using Slope.Report.Models;
using System.Text.Json;


var metadata = MetadataGenerator.Generate<CalculationInput>();

File.WriteAllText(
    "metadata.json",
    JsonSerializer.Serialize(
        metadata,
        new JsonSerializerOptions
        {
            WriteIndented = true
        }));

var app = new MyClass();
app.Run();

Console.ReadLine();

class MyClass
{

    private static Timer? _timer;

    public void Run()
    {
        var watcher = new FileSystemWatcher(@"C:\dev\MyGitHub\calc\Projects")
        {
            IncludeSubdirectories = true,
            Filter = "input.json"
        };

        watcher.Changed += (_, e) =>
        {
            _timer?.Dispose();

            _timer = new Timer(_ =>
            {
                Recalculate(e.FullPath);
            },
            null,
            TimeSpan.FromMilliseconds(500),
            Timeout.InfiniteTimeSpan);
        };

        watcher.EnableRaisingEvents = true;
    }

    private void Recalculate(string inputFile)
    {
        if (!File.Exists(inputFile))
        {
            Console.WriteLine("Missing input.json. Open input.html and save it first.");
            return;
        }

        // 1. Read input
        var json = File.ReadAllText(inputFile);

        var input = JsonSerializer.Deserialize<CalculationInput>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        var slopeGeometry = new SlopeGeometry(input.Points.First());

        var soil = input.Layers.First();

        var actions = input.Loads;



        var calcContext = new CalculationContext
        {
            SlopeGeometry = slopeGeometry,
            Soil = soil,
            Actions = actions
        };

        var outputs = Slope.Helpers.Trace.Start(calcContext);

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
    }





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

    static string TakeInput()
    {
        Console.Write("Enter file name with input: ");
        string input = Console.ReadLine();

        return input ?? string.Empty;
    }
}