using Lab4.Solvers;
using System.Text;

namespace Lab4.UI
{
    internal class ConsoleRunner
    {
        private static readonly (Task, string)[] tasksOrder = [
            (Task.OldestFiles, "GetTheOldestFiles"),
            (Task.DuplicateFilesByNameAndSize, "GetDuplicateFilesByNameAndSize"),
            (Task.FolderDistributionByFileNumber, "GetFolderDistributionByFileNumber"),
            (Task.BiggestFolderBySize, "GetTheBiggestFolderBySize")
        ];

        public static void Main()
        {
            while (true)
            {
                ShowStartMenu();
                char key = Console.ReadKey(true).KeyChar;
                if (key == '0')
                {
                    break;
                }
                switch (key)
                {
                    case '1':
                        RequireFunctionAndProcess(TaskCompletion.Linq);
                        break;
                    case '2':
                        RequireFunctionAndProcess(TaskCompletion.NoLinq);
                        break;
                    default:
                        Console.WriteLine("Такой команды нет!");
                        break;
                }
            }
        }

        private static void RequireFunctionAndProcess(TaskCompletion taskCompletion)
        {
            ShowFunctionsMenu();
            char key = Console.ReadKey(true).KeyChar;
            if (key == '0')
            {
                return;
            }
            int keyInt = key - '1';
            if (keyInt < 0 && keyInt >= tasksOrder.Length)
            {
                Console.WriteLine("Введена неверная команда!");
                return;
            }
            var task = tasksOrder[keyInt].Item1;
            TaskExecutor.ExecuteTask(task, taskCompletion);
        }

        private static void ShowFunctionsMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Выберите одну из функций:");
            
            for (int i = 0; i < tasksOrder.Length; i++)
            {
                var task = tasksOrder[i];
                sb.Append(i + 1).Append(" - ").Append(task.Item2).Append("\n");
            }

            sb.AppendLine("0 - Назад");
            Console.WriteLine(sb.ToString());
        }

        private static void ShowStartMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Выберите одну из опций:")
                .AppendLine("1 - Linq")
                .AppendLine("2 - No Linq")
                .Append("0 - Выход");
            Console.WriteLine(sb.ToString());
        }
    }
}
