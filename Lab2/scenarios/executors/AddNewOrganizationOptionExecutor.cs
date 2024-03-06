using Lab2.core;
using Lab2.entities;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class AddNewOrganizationOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var organizationName = GetOrganizationName();
            var organizationLongitude = GetOrganizationLongitude();
            var organizationLatitude = GetOrganizationLatitude();
            repository.AddOrganization(
                new Organization(
                    0,
                    organizationName,
                    organizationLongitude,
                    organizationLatitude
                )
            );
        }

        private string GetOrganizationName()
        {
            Console.WriteLine("Введите название организации:");
            return Console.ReadLine()!;
        }

        private float GetOrganizationLongitude()
        {
            float? longitude = null;
            while (true)
            {
                Console.WriteLine("Введите долготу организации:");
                longitude = ReadFloatOrNull();
                if (longitude.HasValue)
                {
                    return longitude.Value;
                } else
                {
                    Console.WriteLine("Введено неверное значение!");
                }
            }
        }

        private float GetOrganizationLatitude()
        {
            float? latitude = null;
            while (true)
            {
                Console.WriteLine("Введите широту организации:");
                latitude = ReadFloatOrNull();
                if (latitude.HasValue)
                {
                    return latitude.Value;
                }
                else
                {
                    Console.WriteLine("Введено неверное значение!");
                }
            }
        }

        private float? ReadFloatOrNull()
        {
            try
            {
                return float.Parse(Console.ReadLine()!);
            } catch
            {
                return null;
            }
        }
    }
}
