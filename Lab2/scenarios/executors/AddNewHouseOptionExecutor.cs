using Lab2.core;
using Lab2.entities;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class AddNewHouseOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var city = GetCity();
            var rate = GetRate();
            var name = GetName();
            var organizationId = GetOrganizationId();
            repository.AddHouse(
                new House(
                    0,
                    city,
                    rate,
                    name,
                    organizationId
                )
            );
        }

        private string GetCity()
        {
            Console.WriteLine("Введите название города:");
            return Console.ReadLine()!;
        }

        private int GetRate()
        {
            while (true)
            {
                Console.WriteLine("Введите рейтинг здания:");
                var input = Console.ReadLine()!;
                try
                {
                    var rate = int.Parse(input);
                    if (rate < 0 || rate > 10)
                    {
                        Console.WriteLine("Рейтинг должен быть в диапазоне [0;10]!");
                    } else
                    {
                        return rate;
                    }
                } catch
                {
                    Console.WriteLine("Введено неверное значение!");
                }
            }
        }

        private string GetName()
        {
            Console.WriteLine("Введите название здания:");
            return Console.ReadLine()!;
        }

        private long GetOrganizationId()
        {
            var organizations = repository.GetOrganizations();
            DisplayOrganizations(organizations);
            while (true)
            {
                Console.WriteLine("Введите номер организации:");
                var input = Console.ReadLine()!;
                try
                {
                    var id = long.Parse(input);
                    var organization = repository.GetOrganizationById(id);
                    if (organization == null)
                    {
                        Console.WriteLine("Такой организации не существует!");
                    } else
                    {
                        return id;
                    }
                }
                catch
                {
                    Console.WriteLine("Введено неверное значение!");
                }
            }
        }

        private void DisplayOrganizations(List<OrganizationWithHouses> organizations)
        {
            organizations.ForEach(o =>
            {
                Console.WriteLine(o.ToString());
            });
        }
    }
}
