using Lab1.core;

namespace Lab1.scenarios.impl.settings
{
    internal class SettingsScenario : Scenario
    {
        public ScenarioData ProduceData()
        {
            return new ScenarioData(
                    [
                        CreateBackgroundColorChangeOption(),
                        CreateFontColorChangeOption()
                    ],
                    new ScenarioData.OptionKey('0', "Назад"),
                    "Изменить настройки программы:"
            );
        }

        private static ScenarioData.Option CreateBackgroundColorChangeOption()
        {
            return new ScenarioData.Option(
                new ScenarioData.OptionKey(
                    '1',
                    "Изменить цвет заднего фона"
                ),
                new BackgroundColorChangeExecutor()
            );
        }

        private static ScenarioData.Option CreateFontColorChangeOption()
        {
            return new ScenarioData.Option(
                new ScenarioData.OptionKey(
                    '2',
                    "Изменить цвет шрифта"
                ),
                new FontColorChangeExecutor()
            );
        }

        private class BackgroundColorChangeExecutor() : OptionExecutor
        {
            public void Execute()
            {
                ConsoleInputScenarioProcessor.ProcessScenario(new BackgroundColorChangeScenario());
            }
        }

        private class FontColorChangeExecutor() : OptionExecutor
        {
            public void Execute()
            {
                ConsoleInputScenarioProcessor.ProcessScenario(new FontColorChangeScenario());
            }
        }
    }
}
