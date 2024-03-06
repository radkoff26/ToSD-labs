using Lab2.core;
using Lab2.entities;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class SortHousesByNameOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            Console.WriteLine("Введите 'asc', чтобы отсортировать по убыванию, или 'desc' - по возрастанию:");
            var input = Console.ReadLine();
            if (input != "asc" && input != "desc")
            {
                Console.WriteLine("Введено неверное значение!");
                return;
            }
            Console.WriteLine("Результат запроса:");
            if (input == "asc")
            {
                DisplayHouses(repository.GetHousesSortedByName(false));
            } else
            {
                DisplayHouses(repository.GetHousesSortedByName(true));
            }
        }

        private void DisplayHouses(List<House> houses)
        {
            houses.ForEach(Console.WriteLine);
        }
    }
}
