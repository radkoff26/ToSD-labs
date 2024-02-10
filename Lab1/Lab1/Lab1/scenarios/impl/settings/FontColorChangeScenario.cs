using Lab1.core;

namespace Lab1.scenarios.impl.settings
{
    internal class FontColorChangeScenario : Scenario
    {
        public ScenarioData ProduceData()
        {
            return new ScenarioData(
                options: [
                    CreateColorOption('1', "Красный", ConsoleColor.Red),
                    CreateColorOption('2', "Зелёный", ConsoleColor.Green),
                    CreateColorOption('3', "Синий", ConsoleColor.Blue),
                    CreateColorOption('4', "Чёрный", ConsoleColor.Black),
                    CreateColorOption('5', "Белый", ConsoleColor.White)
                ],
                new ScenarioData.OptionKey('0', "Назад"),
                "Выберите цвет шрифта:"
            );
        }

        private ScenarioData.Option CreateColorOption(char key, string desc, ConsoleColor color)
        {
            return new ScenarioData.Option(new ScenarioData.OptionKey(key, desc), new ChangeColorOptionExecutor(color));
        }

        private class ChangeColorOptionExecutor(ConsoleColor color) : OptionExecutor
        {
            private ConsoleColor color = color;

            public void Execute()
            {
                Console.ForegroundColor = color;
                Console.Clear();
            }
        }
    }
}
