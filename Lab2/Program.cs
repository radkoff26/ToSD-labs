using Lab2.core;
using Lab2.data_source;
using Lab2.entities;
using Lab2.scenarios.impl;

namespace Lab2
{
    // Entry point class
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ConsoleInputScenarioProcessor.ProcessScenario(new ApplicationScenario());
        }
    }
}