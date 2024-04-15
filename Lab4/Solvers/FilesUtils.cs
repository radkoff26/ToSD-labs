namespace Lab4.Solvers
{
    internal class FilesUtils
    {
        public static double CalculateFolderSize(DirectoryInfo directoryInfo)
        {
            double size = 0;

            // Add size of files in the current directory
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                size += file.Length;
            }

            // Add size of subdirectories
            foreach (DirectoryInfo subdirectory in directoryInfo.GetDirectories())
            {
                size += CalculateFolderSize(subdirectory);
            }

            return size;
        }

        public static int CountFiles(DirectoryInfo directory)
        {
            int count = 0;
            count += directory.GetFiles().Length;

            foreach (var subDir in directory.GetDirectories())
            {
                count += CountFiles(subDir);
            }

            return count;
        }

        public static List<FileInfo> GetAllFilesRecursively(DirectoryInfo directoryInfo)
        {
            List<FileInfo> filesInfo = new List<FileInfo>();

            var files = directoryInfo.GetFiles().ToList();

            files.ForEach(filesInfo.Add);

            var dirs = directoryInfo.GetDirectories().ToList();

            dirs.ForEach(d =>
            {
                filesInfo.AddRange(GetAllFilesRecursively(d));
            });

            return filesInfo;
        }
    }
}
