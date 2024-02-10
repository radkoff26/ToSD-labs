using Lab1.core;
using Lab1.scenarios.impl.app;

namespace Lab1
{
    // Entry point class
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ConsoleInputScenarioProcessor.ProcessScenario(new AppScenario());
        }
    }
}