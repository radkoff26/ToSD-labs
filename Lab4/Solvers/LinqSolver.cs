namespace Lab4.Solvers
{
    public class LinqSolver : Solver
    {

        public ValueTuple<string, double> GetTheBiggestFolderBySize(string folder)
        {
            if (!Directory.Exists(folder))
                return ("", 0); // Default value is returned

            List<string> subdirectories = Directory.GetDirectories(folder).ToList();

            if (subdirectories.Count == 0)
                return ("", 0); // Default value is returned

            string biggestDirPath = subdirectories.MaxBy(dir =>
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                return FilesUtils.CalculateFolderSize(directoryInfo);
            })!;

            DirectoryInfo directoryInfo = new DirectoryInfo(biggestDirPath);

            return (directoryInfo.Name, FilesUtils.CalculateFolderSize(directoryInfo));
        }

        public IList<string> GetTheOldestFiles(string folder, int N)
        {
            if (!Directory.Exists(folder))
                return []; // Default value is returned

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            List<FileInfo> files = FilesUtils.GetAllFilesRecursively(directoryInfo);

            if (files.Count == 0)
                return []; // Default value is returned

            return files.OrderBy(f => f.CreationTime).Take(N).Select(f => f.FullName).ToList();
        }

        public IDictionary<int, IList<string>> GetFolderDistributionByFileNumber(string folder)
        {
            if (!Directory.Exists(folder))
                return new Dictionary<int, IList<string>>(); // Default value is returned

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            List<DirectoryInfo> subDirectories = directoryInfo.GetDirectories().ToList();

            return subDirectories.GroupBy(FilesUtils.CountFiles)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => f.FullName).ToList() as IList<string>
                );
        }

        public IList<ValueTuple<IList<string>, double>> GetDuplicateFilesByNameAndSize(string folder)
        {
            if (!Directory.Exists(folder))
                return []; // Default value is returned

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);

            var filesList = FilesUtils.GetAllFilesRecursively(directoryInfo);

            return filesList.GroupBy(
                f => Path.GetFileName(f.FullName),
                (_, files) => files.GroupBy(
                    f => (double)f.Length,
                    (size, files) => new
                    {
                        Size = size,
                        FilePaths = files.Select(f => f.FullName).ToList()
                    }
                )
            )
            .SelectMany(items =>
            {
                return items.Select(item => new ValueTuple<IList<string>, double>(item.FilePaths, item.Size));
            })
            .ToList();
        }
    }
}