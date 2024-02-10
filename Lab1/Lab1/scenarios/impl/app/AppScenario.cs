using Lab1.core;
using Lab1.scenarios.impl.settings;
using Lab1.scenarios.impl.type;

namespace Lab1.scenarios.impl.app
{
    internal class AppScenario : Scenario
    {
        public ScenarioData ProduceData()
        {
            return new ScenarioData(
                options: [
                    new ScenarioData.Option(
                        new ScenarioData.OptionKey('1', "Общая информация по типам"),
                        new GeneralTypesInfoExecutor()
                    ),
                    new ScenarioData.Option(
                        new ScenarioData.OptionKey('2', "Выбрать тип из списка"),
                        new TypeSelectionExecutor()
                    ),
                    new ScenarioData.Option(
                        new ScenarioData.OptionKey('3', "Параметры консоли"),
                        new ConsoleSettingsExecutor()
                    )
                ],
                new ScenarioData.OptionKey('0', "Выход из программы"),
                "Информация по типам:"
            );
        }

        private class GeneralTypesInfoExecutor : OptionExecutor
        {
            public void Execute()
            {
                GeneralTypesInfoExtractor.ShowTypesInfo();
            }
        }

        private class TypeSelectionExecutor : OptionExecutor
        {
            public void Execute()
            {
                ConsoleInputScenarioProcessor.ProcessScenario(new TypeSelectionScenario());
            }
        }

        private class ConsoleSettingsExecutor : OptionExecutor
        {
            public void Execute()
            {
                ConsoleInputScenarioProcessor.ProcessScenario(new SettingsScenario());
            }
        }
    }
}
