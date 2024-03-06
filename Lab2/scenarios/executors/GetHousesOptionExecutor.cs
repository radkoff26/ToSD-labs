using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetHousesOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var houses = repository.GetHouses();
            Console.WriteLine("Результат запроса:");
            houses.ForEach(Console.WriteLine);
        }
    }
}
