namespace Lab4.Solvers
{
    public interface Solver
    {

        // Самая большая по размеру вложенная папка и размер в Мб
        ValueTuple<string, double> GetTheBiggestFolderBySize(string folder);
        // N самых старых файлов (по дате создания)
        IList<string> GetTheOldestFiles(string folder, int N);
        // Файлы-дубликаты, у которых совпадает имя с расширением и размер
        IList<ValueTuple<IList<string>, double>> GetDuplicateFilesByNameAndSize(string folder);
        // Распределение вложенных папок по количеству файлов
        IDictionary<int, IList<string>> GetFolderDistributionByFileNumber(string folder);
    }
}
