using Slope.Engnes;
using Slope.EngnesAndGenerators;
using Slope.Models;
using Slope.Models.ConsoleUI;
using Slope.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.UI
{
    public class ConsoleMenu
    {
        private readonly ProjectService _projectService;
        private readonly CalculationEngine _calculationEngine;
        private readonly ReportGenerator _reportGenerator;

        private CalculationInput _input;
        private string _projectFolder;

        private readonly MenuContext _context = new();

        private readonly Dictionary<string, MenuCommand> _commands = new Dictionary<string, MenuCommand>
        {
           { "1", new OpenProjectCommand()},
           { "2", new NewProjectCommand()},
           { "3", new EditGeometryCommand()},
           { "4", new EditLayersCommand()},
           { "5", new EditLoadsCommand()},
           { "6", new CalculateCommand()},
           { "7", new OpenReportCommand()},
           { "0", new ExitCommand() }
        };

        public ConsoleMenu(ProjectService projectService, CalculationEngine calculationEngine, ReportGenerator reportGenerator)
        {
            _projectService = projectService;
            _calculationEngine = calculationEngine;
            _reportGenerator = reportGenerator;
        }

        public void Run()
        {
            while (_context.IsRunning)
            {
                Draw();

                Console.Write("> ");

                var key = Console.ReadLine()?.Trim();

                if(key == null || !_commands.TryGetValue(key, out MenuCommand? command))
                {
                    continue;
                }

                if (command == null)
                {
                    continue ;
                }

                if (!command.CanExecute(_context))
                {
                    Console.WriteLine();
                    Console.WriteLine("Command is not available.");
                    Pause();
                    continue;
                }

                Console.Clear();

                command.Execute(_context);
                //Pause() ;
            }
        }

        private void Draw()
        {
            Console.Clear();

            Console.WriteLine("=========================================");
            Console.WriteLine("      Slope Stability Calculator");
            Console.WriteLine("=========================================");
            Console.WriteLine();

            Console.WriteLine($"Project : {_context.ProjectFolder ?? "<none>"}");

            Console.WriteLine();

            foreach (var kvp in _commands)
            {
                var command = kvp.Value;
                command.Key = kvp.Key;

                var enabled = command.CanExecute(_context);

                Console.WriteLine(
                    $"{command.Key}. {command.Title}" +
                    (enabled ? "" : " (disabled)"));
            }

            Console.WriteLine();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press ENTER...");
            Console.ReadLine();
        }
    }
}
