using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetOrganizationsFilteredInLocationRegionOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            try
            {
                float fromLatitude = ReadFloat("Введите минимальную границу широты:");
                float toLatitude = ReadFloat("Введите максимальную границу широты");
                float fromLongitude = ReadFloat("Введите минимальную границу долготы:");
                float toLongitude = ReadFloat("Введите максимальную границу долготы");
                var organizations = repository.GetOrganizationsFilteredByLocationRange(fromLatitude, toLatitude, fromLongitude, toLongitude);
                Console.WriteLine("Результат запроса:");
                organizations.ForEach(organ => Console.WriteLine(organ.ToString()));
            } catch
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }

        private float ReadFloat(string prompt)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine()!;
            return float.Parse(input);
        }
    }
}
