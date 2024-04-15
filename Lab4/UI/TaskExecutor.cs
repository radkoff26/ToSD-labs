using Lab4.Solvers;
using System.IO;
using System.Text;

namespace Lab4.UI
{
    internal class TaskExecutor
    {
        private static LinqSolver linqSolver = new LinqSolver();
        private static NoLinqSolver noLinqSolver = new NoLinqSolver();

        public static void ExecuteTask(Task task, TaskCompletion taskCompletion)
        {
            Solver solver = GetSolverForCompletion(taskCompletion);
            ProcessTask(task, solver, taskCompletion);
        }

        private static void ProcessTask(Task task, Solver solver, TaskCompletion completion)
        {
            string functionName = "";
            Func<string> func = () => { return ""; };
            switch (task)
            {
                case Task.OldestFiles:
                    functionName = "GetTheOldestFiles";
                    func = () => {
                        Console.WriteLine("Введите путь к папке:");
                        string dirPath = Console.ReadLine()!;

                        Console.WriteLine("Введите количество выводимых файлов:");
                        int filesLimit = int.Parse(Console.ReadLine()!);

                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        var result = solver.GetTheOldestFiles(dirPath, filesLimit);
                        watch.Stop();

                        var elapsedMs = watch.ElapsedMilliseconds;
                        Console.WriteLine("Затраченное время (мс): " + elapsedMs);

                        return string.Join(", ", result);
                    };
                    break;
                case Task.DuplicateFilesByNameAndSize:
                    functionName = "GetDuplicateFilesByNameAndSize";
                    func = () => {
                        Console.WriteLine("Введите путь к папке:");
                        string dirPath = Console.ReadLine()!;

                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        var result = solver.GetDuplicateFilesByNameAndSize(dirPath);
                        watch.Stop();

                        var elapsedMs = watch.ElapsedMilliseconds;
                        Console.WriteLine("Затраченное время (мс): " + elapsedMs);

                        StringBuilder sb = new StringBuilder();

                        foreach (var item in result)
                        {
                            sb.AppendLine("------");
                            sb.Append("Размер (в байтах): ").Append(item.Item2).Append("\n");
                            sb.Append("Файлы: ").Append(string.Join(", ", item.Item1)).Append("\n");
                        }
                        return sb.ToString();
                    };
                    break;
                case Task.FolderDistributionByFileNumber:
                    functionName = "GetFolderDistributionByFileNumber";
                    func = () => {
                        Console.WriteLine("Введите путь к папке:");
                        string dirPath = Console.ReadLine()!;

                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        var result = solver.GetFolderDistributionByFileNumber(dirPath);
                        watch.Stop();

                        var elapsedMs = watch.ElapsedMilliseconds;
                        Console.WriteLine("Затраченное время (мс): " + elapsedMs);

                        StringBuilder sb = new StringBuilder();

                        foreach (var item in result)
                        {
                            sb.AppendLine("------");
                            sb.Append("Количество файлов в директории: ").Append(item.Key).Append("\n");
                            sb.Append("Файлы: ").Append(string.Join(", ", item.Value)).Append("\n");
                        }
                        return sb.ToString();
                    };
                    break;
                case Task.BiggestFolderBySize:
                    functionName = "GetTheBiggestFolderBySize";
                    func = () => {
                        Console.WriteLine("Введите путь к папке:");
                        string dirPath = Console.ReadLine()!;

                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        var result = solver.GetTheBiggestFolderBySize(dirPath);
                        watch.Stop();

                        var elapsedMs = watch.ElapsedMilliseconds;
                        Console.WriteLine("Затраченное время (мс): " + elapsedMs);

                        StringBuilder sb = new StringBuilder();

                        return sb.Append("Самая большая вложненная директория: ")
                            .Append(result.Item1)
                            .Append("\n")
                            .Append("Размер (в МБ): ")
                            .Append((int) (result.Item2 / (1024 * 1024)))
                            .ToString();
                    };
                    break;
            }
            InvokeMethodAndDisplayOutput(func, completion, functionName);
        }

        private static void InvokeMethodAndDisplayOutput(Func<string> func, TaskCompletion completion, string functionName)
        {
            string output = func.Invoke();
            Console.WriteLine("Результат: ");
            Console.WriteLine(output);
            WriteOutputToFile(output, completion, functionName);
            Console.WriteLine("Запись в файл прошла успешно!");
        }

        private static void WriteOutputToFile(string output, TaskCompletion completion, string functionName)
        {
            StringBuilder stringBuilder = new StringBuilder("./");
            switch (completion)
            {
                case TaskCompletion.Linq:
                    stringBuilder.Append("LinqSolverResults/");
                    break;
                case TaskCompletion.NoLinq:
                    stringBuilder.Append("NoLinqSolverResults/");
                    break;
            }
            EnsureDirIsCreated(stringBuilder.ToString());
            stringBuilder.Append(functionName).Append(".txt");
            string pathToFile = stringBuilder.ToString();
            FileStream stream = new FileStream(pathToFile, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.Write(output);
            streamWriter.Flush();
            streamWriter.Close();
            stream.Close();
        }

        private static void EnsureDirIsCreated(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            if (directory.Exists) { return; }
            directory.Create();
        }

        private static Solver GetSolverForCompletion(TaskCompletion taskCompletion)
        {
            switch (taskCompletion)
            {
                case TaskCompletion.Linq:
                    return linqSolver;
                case TaskCompletion.NoLinq:
                    return noLinqSolver;
            }
            throw new Exception("Illegal state!");
        }
    }
}
