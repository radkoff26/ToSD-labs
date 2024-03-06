using System.Text;

namespace Lab2.core
{
    internal struct ScenarioData(ScenarioData.Option[] options, ScenarioData.OptionKey exitKey, string headerText)
    {
        public Option[] Options { get; } = options;
        public OptionKey ExitKey { get; } = exitKey;
        public string HeaderText { get; } = headerText;

        public struct Option(OptionKey key, OptionExecutor executor)
        {
            public OptionKey Key { get; } = key;
            public OptionExecutor Executor { get; } = executor;
        }

        public struct OptionKey(char key, string description)
        {
            public char Key { get; } = key;
            public string Description { get; } = description;

            public override string ToString()
            {
                return Key + " - " + Description;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HeaderText).Append("\n");
            foreach (var option in Options)
            {
                sb.Append(option.Key.ToString()).Append("\n");
            }
            return sb.Append(ExitKey.ToString()).ToString();
        }
    }
}
