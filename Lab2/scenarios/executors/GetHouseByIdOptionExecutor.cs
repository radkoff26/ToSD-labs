using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetHouseByIdOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            Console.WriteLine("Введите номер здания:");
            var input = Console.ReadLine()!;
            try
            {
                var id = long.Parse(input);
                var house = repository.GetHouseById(id);
                Console.WriteLine("Результат запроса:");
                if (house == null)
                {
                    Console.WriteLine("Здание не найдено!");
                } else
                {
                    Console.WriteLine(house);
                }
            } catch
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }
    }
}
