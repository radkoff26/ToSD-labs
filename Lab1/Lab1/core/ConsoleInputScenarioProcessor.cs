using Lab1.scenarios;

namespace Lab1.core
{
    internal class ConsoleInputScenarioProcessor
    {
        public static void ProcessScenario(Scenario scenario)
        {
            ScenarioData scenarioData = scenario.ProduceData();
            char currentKey;
            Console.Clear();
            while ((currentKey = AskForAndGetNextCharBlocking(scenarioData)) != scenarioData.ExitKey.Key)
            {
                bool isFound = false;
                foreach (var option in scenarioData.Options)
                {
                    if (currentKey == option.Key.Key)
                    {
                        isFound = true;
                        option.Executor.Execute();
                        break;
                    }
                }
                if (!isFound) { 
                    NotifyAboutIncorrectInput();
                }
                ShowDivider();
            }
            NotifyAboutProgramExit();
        }

        private static void ShowDivider()
        {
            Console.WriteLine("------------------");
        }

        private static char AskForAndGetNextCharBlocking(ScenarioData data)
        {
            AskForInput(data);
            return InputUtils.GetCharBlocking();
        }

        private static void AskForInput(ScenarioData data)
        {
            Console.WriteLine(data);
            Console.WriteLine("Выберите команду для продолжения...");
        }

        private static void NotifyAboutIncorrectInput()
        {
            Console.WriteLine("Неизвестный символ введён!");
        }

        private static void NotifyAboutProgramExit()
        {
            Console.WriteLine("Выход...");
        }
    }
}
