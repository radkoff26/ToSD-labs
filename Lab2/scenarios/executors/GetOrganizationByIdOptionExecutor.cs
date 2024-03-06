using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetOrganizationByIdOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            Console.WriteLine("Введите номер организации:");
            var input = Console.ReadLine()!;
            try
            {
                var id = long.Parse(input);
                var organization = repository.GetOrganizationById(id);
                Console.WriteLine("Результат запроса:");
                if (organization == null)
                {
                    Console.WriteLine("Организация не найдена!");
                } else
                {
                    Console.WriteLine(organization.ToString());
                }
            } catch
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }
    }
}
