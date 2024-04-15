namespace Lab4.Solvers
{
    public class NoLinqSolver : Solver
    {

        public ValueTuple<string, double> GetTheBiggestFolderBySize(string folder)
        {
            if (!Directory.Exists(folder))
                return ("", 0); // Default value is returned

            string[] subdirectories = Directory.GetDirectories(folder);

            if (subdirectories.Length == 0)
                return ("", 0); // Default value is returned

            string biggestFolder = string.Empty;
            double maxSize = 0;

            foreach (string subdirectory in subdirectories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(subdirectory);
                double size = FilesUtils.CalculateFolderSize(directoryInfo);

                if (size > maxSize)
                {
                    maxSize = size;
                    biggestFolder = directoryInfo.Name;
                }
            }

            return (biggestFolder, maxSize);
        }

        public IList<string> GetTheOldestFiles(string folder, int N)
        {
            if (!Directory.Exists(folder))
                return []; // Default value is returned

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            List<FileInfo> files = FilesUtils.GetAllFilesRecursively(directoryInfo);

            if (files.Count == 0)
                return []; // Default value is returned

            files.Sort((f1, f2) => DateTime.Compare(f1.CreationTime, f2.CreationTime));

            List<string> oldestFiles = new List<string>();
            for (int i = 0; i < Math.Min(N, files.Count); i++)
            {
                oldestFiles.Add(files[i].FullName);
            }

            return oldestFiles;
        }

        public IDictionary<int, IList<string>> GetFolderDistributionByFileNumber(string folder)
        {
            if (!Directory.Exists(folder))
                return new Dictionary<int, IList<string>>(); // Default value is returned

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();

            IDictionary<int, IList<string>> distribution = new Dictionary<int, IList<string>>();

            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                int fileCount = FilesUtils.CountFiles(subDirectory);
                if (!distribution.ContainsKey(fileCount))
                {
                    distribution[fileCount] = new List<string>();
                }
                distribution[fileCount].Add(subDirectory.FullName);
            }

            return distribution;
        }

        public IList<ValueTuple<IList<string>, double>> GetDuplicateFilesByNameAndSize(string folder)
        {
            if (!Directory.Exists(folder))
                return []; // Default value is returned

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);

            var filesList = FilesUtils.GetAllFilesRecursively(directoryInfo);
            var filesMap = new Dictionary<string, Dictionary<double, IList<string>>>();

            filesList.ForEach(f =>
            {
                string filePath = Path.GetFileName(f.FullName);
                if (!filesMap.ContainsKey(filePath))
                {
                    filesMap[filePath] = [];
                }
                var innerFilesMap = filesMap[filePath];
                if (!innerFilesMap.ContainsKey(f.Length))
                {
                    innerFilesMap[f.Length] = new List<string>();
                }
                var innerFilesList = innerFilesMap[f.Length];
                innerFilesList.Add(f.FullName);
            });

            List<ValueTuple<IList<string>, double>> result = [];

            foreach (var group in filesMap)
            {
                var innerDict = group.Value;

                foreach (var similar in innerDict)
                {
                    result.Add((similar.Value, similar.Key));
                }
            }

            return result;
        }
    }
}