using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class DeleteHouseByIdOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var houses = repository.GetHouses();
            houses.ForEach(Console.WriteLine);
            Console.WriteLine("Введите номер удаляемого здания:");
            var input = Console.ReadLine()!;
            try
            {
                var id = long.Parse(input);
                var isDeleted = repository.DeleteHouseById(id);
                if (isDeleted)
                {
                    Console.WriteLine("Успешно удалено!");
                }
                else
                {
                    Console.WriteLine("Такого здания не существует!");
                }
            }
            catch
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }
    }
}
