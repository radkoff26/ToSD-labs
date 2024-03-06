using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class CountHousesOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var housesCount = repository.CountHouses();
            Console.WriteLine("Результат");
            Console.WriteLine(housesCount);
        }
    }
}
