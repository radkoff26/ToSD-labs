using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetAverageHousesRateOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            float averageRate = repository.GetAverageHousesRating();
            Console.WriteLine("Результат запроса:");
            Console.WriteLine(averageRate);
        }
    }
}
