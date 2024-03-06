using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetHousesByCityOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            Console.WriteLine("Введите название города:");
            var city = Console.ReadLine()!;
            var houses = repository.GetHousesByCity(city);
            Console.WriteLine("Результат запроса:");
            houses.ForEach(Console.WriteLine);
        }
    }
}
